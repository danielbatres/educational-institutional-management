using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.DTO;
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
  [Route("create")]
  public async Task<IActionResult> Post([FromBody] UserDto userDto) {
    await userService.Create(userDto);

    return Ok();
  }

  [HttpGet]
  [Route("createdb")]
  public IActionResult CreateDatabase()
  {
    context.Database.EnsureCreated();

    return Ok();
  }
}