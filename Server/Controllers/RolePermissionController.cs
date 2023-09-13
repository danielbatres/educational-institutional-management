using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolePermissionController : ControllerBase {
  private readonly IRolePermissionService _rolePermissionService;

  public RolePermissionController(IRolePermissionService rolePermissionService) {
    _rolePermissionService = rolePermissionService;
  }
  
  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Post([FromBody] RolePermission rolePermission) {
    await _rolePermissionService.Create(rolePermission);
  
      return Ok();
  }
  
  [HttpDelete]
  [Route("remove")]
  public async Task<IActionResult> Delete([FromBody] RolePermission rolePermission) {
    await _rolePermissionService.Delete(rolePermission);
  
    return Ok();
  }
}