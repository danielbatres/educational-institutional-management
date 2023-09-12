using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivityController : ControllerBase {
  private readonly IActivityService _activityService;

  public ActivityController(IActivityService activityService) {
    _activityService = activityService;
  }

  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Create([FromBody] ActivityLog activity) {
    await _activityService.Create(activity);

    return Ok();
  }
}