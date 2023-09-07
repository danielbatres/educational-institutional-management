using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase {
  private readonly IUserService userService;
  public UserController(IUserService service) {
    userService = service;
  }

  [HttpGet]
  public IActionResult Get() {
    return Ok(userService.Get());
  }

  [HttpPost]
  [Route("login-user")]
  public async Task<ActionResult<User>> LoginUser(User user) {
    var claim = new Claim(ClaimTypes.Name, user.Id.ToString());
    var claimsIdentity = new ClaimsIdentity(new[] { claim }, "serverAuth");
    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

    await HttpContext.SignInAsync(claimsPrincipal);

    return await Task.FromResult(user);
  }

  [HttpGet]
  [Route("get-current-user")]
  public async Task<ActionResult<User>> GetCurrentUser() {
    User currentUser = new();

    if (User.Identity.IsAuthenticated) {
      currentUser = userService.Get().Where(x => x.Id == Guid.Parse(User.FindFirstValue(ClaimTypes.Name))).ToList()[0];
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
    await userService.Create(user);

    return Ok();
  }

  [HttpGet("GoogleSignIn")]
  public async Task GoogleSignIn() {
    await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = "/" });
  }

  [HttpPut]
  [Route("update")]
  public async Task<IActionResult> Put([FromBody] User user) {
    await userService.Update(user);

    return Ok();
  }

  private bool VerifyPassword(string storedPassword, string passwordAttempt)
  {
    string[] parts = storedPassword.Split(':');
    if (parts.Length != 2)
      return false;

    byte[] saltBytes = Convert.FromBase64String(parts[1]);
    byte[] passwordAttemptBytes = Encoding.UTF8.GetBytes(passwordAttempt);
    byte[] combinedBytes = new byte[passwordAttemptBytes.Length + saltBytes.Length];
    Buffer.BlockCopy(passwordAttemptBytes, 0, combinedBytes, 0, passwordAttemptBytes.Length);
    Buffer.BlockCopy(saltBytes, 0, combinedBytes, passwordAttemptBytes.Length, saltBytes.Length);

    using var sha256 = SHA256.Create();
    byte[] hashedAttemptBytes = sha256.ComputeHash(combinedBytes);
    string hashedAttempt = Convert.ToBase64String(hashedAttemptBytes);

    return parts[0] == hashedAttempt;
  }
}