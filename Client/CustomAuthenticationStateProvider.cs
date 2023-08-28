using Microsoft.AspNetCore.Components.Authorization;

namespace edu_institutional_management.Client;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
      throw new NotImplementedException();
    }
}