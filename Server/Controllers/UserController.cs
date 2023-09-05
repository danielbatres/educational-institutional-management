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
  private readonly MainContext context;
  public UserController(IUserService service, MainContext mainContext) {
    userService = service;
    context = mainContext;
  }

  [HttpGet]
  public IActionResult Get() {
    return Ok(userService.Get());
  }

  [HttpPost]
  [Route("login-user")]
  public async Task<ActionResult<User>> LoginUser(User user) {
    var claim = new Claim(ClaimTypes.Name, user.Register.Email);
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
      currentUser.Register = new() {
        Email = User.FindFirstValue(ClaimTypes.Name)
      };
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

  [HttpGet]
  [Route("createdb")]
  public IActionResult CreateDatabase()
  {
    context.Database.EnsureCreated();

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