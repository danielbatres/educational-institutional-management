using edu_institutional_management.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionController : ControllerBase {
  private readonly IPermissionService _permissionService;
  
  public PermissionController(IPermissionService permissionService) {
    _permissionService = permissionService;
  }

  [HttpGet]
  public IActionResult Get() {
    return Ok(_permissionService.Get());
  }
}