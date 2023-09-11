using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class RoleController : ControllerBase {
  private readonly IRoleService _roleService;

  public RoleController(IRoleService roleService) {
    _roleService = roleService;
  }

  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Post([FromBody] Role role) {
    await _roleService.Create(role);

    return Ok();
  }
}