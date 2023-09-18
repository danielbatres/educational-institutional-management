using System.Security.Claims;
using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase {
  private readonly IUserService _userService;
  
  public UserController(IUserService service) {
    _userService = service;
  }

  [HttpGet]
  public IActionResult Get() {
    return Ok(_userService.Get());
  }

  [HttpPost]
  [Route("login-user")]
  public async Task<ActionResult<User>> LoginUser(User user, bool isPersistent) {
    var claim = new Claim(ClaimTypes.Name, user.Id.ToString());
    var claimsIdentity = new ClaimsIdentity(new[] { claim }, "serverAuth");
    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

    await HttpContext.SignInAsync(claimsPrincipal, GetAuthenticationProperties(isPersistent));

    return await Task.FromResult(user);
  }

  [HttpGet]
  [Route("get-current-user")]
  public async Task<ActionResult<User>> GetCurrentUser() {
    User currentUser = new();

    if (User.Identity.IsAuthenticated) {
      currentUser = _userService.Get().Where(x => x.Id == Guid.Parse(User.FindFirstValue(ClaimTypes.Name))).ToList()[0];
    }

    return await Task.FromResult(currentUser);
  }

  [HttpGet]
  [Route("logout-user")]
  public async Task<IActionResult> LogoutUser() {
    await HttpContext.SignOutAsync();

    return Ok();
  }

  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Post([FromBody] User user) {
    await _userService.Create(user);

    return Ok();
  }

  [HttpGet]
  [Route("google-sign-in-callback")]
  public async Task<IActionResult> GoogleSignInCallback() {
    var googleUser = await HttpContext.AuthenticateAsync("Google");

    if (googleUser.Succeeded) {
      var user = googleUser.Principal;

      Guid UserId = Guid.NewGuid();

      User User = new() {
        Id = UserId,
        Name = user.FindFirst(ClaimTypes.Name)?.Value,
        Location = user.FindFirst(ClaimTypes.Locality)?.Value,
        Register = new() {
          Id = Guid.NewGuid(),
          Email = user.FindFirst(ClaimTypes.Email)?.Value,
          AuthenticationMethod = "GoogleAuthentication",
          UserId = UserId
        },
        OnlineStatus = new() {
          Id = Guid.NewGuid(),
          Status = true,
          LastConnection = DateTime.Now,
          UserId = UserId
        }
      };

      await Post(User);
      await LoginUser(User, true);

      HttpContext.Response.Redirect("/");
    }

    return Ok();
  }

  [HttpGet("google-sign-in")]
  public IActionResult GoogleSignIn() {
    var properties = new AuthenticationProperties { RedirectUri = "/api/user/google-sign-in-callback" };
    return Challenge(properties, "Google");
  }

  [HttpPut]
  [Route("update")]
  public async Task<IActionResult> Put([FromBody] User user) {
    await _userService.Update(user);

    return Ok();
  }

  private AuthenticationProperties GetAuthenticationProperties(bool isPersistent = false) {
    return new AuthenticationProperties() {
      IsPersistent = isPersistent,
      ExpiresUtc = DateTime.UtcNow.AddDays(5),
    };
  }
}