using System.Net.Http.Json;
using System.Security.Claims;
using edu_institutional_management.Client.Containers;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace edu_institutional_management.Client;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider {

  private readonly HttpClient _httpClient;
  private readonly UserContext _userContext;

  public CustomAuthenticationStateProvider(HttpClient httpClient, UserContext userContext) {
    _httpClient = httpClient;
    _userContext = userContext;
  }
  public async override Task<AuthenticationState> GetAuthenticationStateAsync()
  {
    User currentUser = await _httpClient.GetFromJsonAsync<User>("api/user/get-current-user");

    if (!currentUser.Id.Equals(Guid.Empty)) {
      _userContext.SetUser(currentUser);
      _userContext.SetIsActiveUser(true);
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