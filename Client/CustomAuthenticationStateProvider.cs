using System.Net.Http.Json;
using System.Security.Claims;
using edu_institutional_management.Client.Containers;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace edu_institutional_management.Client;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider {

  private readonly HttpClient _httpClient;

  public CustomAuthenticationStateProvider(HttpClient httpClient) {
    _httpClient = httpClient;
  }
  public async override Task<AuthenticationState> GetAuthenticationStateAsync()
  {
    User currentUser = await _httpClient.GetFromJsonAsync<User>("api/user/get-current-user");

    if (currentUser != null && currentUser.Register != null && currentUser.Register.Email != null) {
      var claim = new Claim(ClaimTypes.Name, currentUser.Register.Email);
      var claimsIdentity = new ClaimsIdentity(new[] { claim }, "serverAuth");
      var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

      return new AuthenticationState(claimsPrincipal);
    } else {
      return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }
  }
}