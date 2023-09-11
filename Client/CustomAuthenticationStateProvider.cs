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
  private IInstitutionService _institutionService;
  private RolesHubManager _rolesHubManager;

  public CustomAuthenticationStateProvider(HttpClient httpClient, UserContext userContext, NavigationManager navigationManager, IInstitutionService institutionService, RolesHubManager rolesHubManager) {
    _httpClient = httpClient;
    _userContext = userContext;
    _navigationManager = navigationManager;
    _institutionService = institutionService;
    _rolesHubManager = rolesHubManager;
  }

  public async override Task<AuthenticationState> GetAuthenticationStateAsync() {
    User currentUser = await _httpClient.GetFromJsonAsync<User>("api/user/get-current-user");

    if (!currentUser.Id.Equals(Guid.Empty)) {
      _userContext.SetUser(currentUser);
      _userContext.SetIsActiveUser(true);

      if (_userContext.User.InstitutionId != null) {
        _userContext.NavigateToInstitution();
        await _institutionService.SendInstitutionConnection(new DataBaseConnectionRequest() {
          DataBaseName = _userContext.User.Institution?.DataBaseConnectionName ?? string.Empty
        });
        
        await _rolesHubManager.SendGroupName(_userContext.User.Institution.Name);
      }
    }

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