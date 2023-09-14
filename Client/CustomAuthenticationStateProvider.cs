using System.Net.Http.Json;
using System.Security.Claims;
using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace edu_institutional_management.Client;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider {

  private readonly HttpClient _httpClient;
  private readonly UserContext _userContext;
  private readonly NavigationManager _navigationManager;
  private readonly IInstitutionService _institutionService;
  private readonly ISettingsService _settingsService;
  private readonly RolesHubManager _rolesHubManager;
  private readonly CategoryHubManager _categoryHubManager;
  private readonly StudentHubManager _studentHubManager;
  private readonly ActivityHubManager _activityHubManager;
  private readonly ThemeContext _themeContext;
  private readonly LoadingSiteContext _loadingContext;

  public CustomAuthenticationStateProvider(HttpClient httpClient, UserContext userContext, NavigationManager navigationManager, IInstitutionService institutionService, RolesHubManager rolesHubManager, CategoryHubManager categryHubManager, ISettingsService settingsService, ThemeContext themeContext, LoadingSiteContext loadingContext, StudentHubManager studentHubManager, ActivityHubManager activityHubManager) {
    _httpClient = httpClient;
    _userContext = userContext;
    _navigationManager = navigationManager;
    _institutionService = institutionService;
    _rolesHubManager = rolesHubManager;
    _categoryHubManager = categryHubManager;
    _settingsService = settingsService;
    _themeContext = themeContext;
    _loadingContext = loadingContext;
    _studentHubManager = studentHubManager;
    _activityHubManager = activityHubManager;
  }

  public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
    User currentUser = await _httpClient.GetFromJsonAsync<User>("api/user/get-current-user");

    if (!currentUser.Id.Equals(Guid.Empty)) {
      _userContext.SetUser(currentUser);
      _userContext.SetIsActiveUser(true);

      if (_userContext.User.InstitutionId != null) {
        _userContext.NavigateToInstitution();
        await _institutionService.SendInstitutionConnection(new DataBaseConnectionRequest() {
          DataBaseName = _userContext.User.Institution?.DataBaseConnectionName ?? string.Empty
        });

        await ConnectHubs(_userContext.User.InstitutionId.ToString());
      }
    }
    
    _loadingContext.SetLoading(false);
    
    if (currentUser != null) {
      var claim = new Claim(ClaimTypes.Name, currentUser.Id.ToString());
      var claimsIdentity = new ClaimsIdentity(new[] { claim }, "serverAuth");
      var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

      return new AuthenticationState(claimsPrincipal);
    } else {
      return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }
  }

  private async Task ConnectHubs(string groupName) {
    await _rolesHubManager.StartConnectionAsync();
    await _categoryHubManager.StartConnectionAsync();
    await _studentHubManager.StartConnectionAsync();
    await _activityHubManager.StartConnectionAsync();

    await _rolesHubManager.JoinGroup(groupName);
    await _categoryHubManager.JoinGroup(groupName);
    await _studentHubManager.JoinGroup(groupName);
    await _activityHubManager.JoinGroup(groupName);
  }
}