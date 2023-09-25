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
  private readonly IDatabaseService _databaseService;
  private readonly UserContext _userContext;
  private readonly NavigationManager _navigationManager;
  private readonly IInstitutionService _institutionService;
  private readonly ISettingsService _settingsService;
  private readonly ThemeContext _themeContext;
  private readonly LoadingSiteContext _loadingContext;
  private readonly IUserService _userService;
  private readonly HubsConnection _hubsConnection;
  private readonly UsersHubManager _usersHubManager;

  public CustomAuthenticationStateProvider(HttpClient httpClient, IDatabaseService databaseService, UserContext userContext, NavigationManager navigationManager, IInstitutionService institutionService, ISettingsService settingsService, ThemeContext themeContext, LoadingSiteContext loadingContext, IUserService userService, HubsConnection hubsConnection, UsersHubManager usersHubManager) {
    _httpClient = httpClient;
    _databaseService = databaseService;
    _userContext = userContext;
    _navigationManager = navigationManager;
    _institutionService = institutionService;
    _settingsService = settingsService;
    _themeContext = themeContext;
    _loadingContext = loadingContext;
    _userService = userService;
    _hubsConnection = hubsConnection;
    _usersHubManager = usersHubManager;
  }

  public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
    await _databaseService.EnsureMainDb();

    User currentUser = await _httpClient.GetFromJsonAsync<User>("api/user/get-current-user");

    if (!currentUser.Id.Equals(Guid.Empty)) {
      _userContext.SetUser(currentUser);
      _userContext.SetIsActiveUser(true);

      currentUser.OnlineStatus.Status = true;
      currentUser.OnlineStatus.LastConnection = DateTime.Now;
      await _userService.Update(currentUser);

      if (_userContext.User.InstitutionId != null) {
        _userContext.NavigateToInstitution((Guid) _userContext.User.InstitutionId);
        await _institutionService.SendInstitutionConnection(_userContext.User.Institution?.DataBaseConnectionName ?? string.Empty);

        string groupName = _userContext.User.InstitutionId.ToString() ?? string.Empty;

        await _hubsConnection.ConnectHubs(groupName);
        await _usersHubManager.SendUsersUpdatedAsync(groupName);
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
}