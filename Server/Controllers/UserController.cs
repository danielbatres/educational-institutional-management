using edu_institutional_management.Server.Services;
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
}