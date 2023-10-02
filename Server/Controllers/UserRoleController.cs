using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserRoleController : ControllerBase {
  private readonly IUserRoleService _userRoleService;
  
  public UserRoleController(IUserRoleService userRoleService) {
    _userRoleService = userRoleService;
  }
  
  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Post([FromBody] UserRole userRole) {
    await _userRoleService.Create(userRole);
    
    return Ok();
  }
  
  [HttpDelete]
  [Route("remove")]
  public async Task<IActionResult> Delete(string userId) {
    await _userRoleService.Delete(Guid.Parse(userId));
    
    return Ok();
  }
}