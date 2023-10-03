using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivityViewController : ControllerBase {
  private readonly IActivityLogViewService _activityLogViewService;

  public ActivityViewController(IActivityLogViewService activityLogViewService) {
    _activityLogViewService = activityLogViewService;
  }

  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Post([FromBody] ActivityLogView activityLogView) {
    await _activityLogViewService.Create(activityLogView);

    return Ok();
  }

  [HttpPut]
  [Route("update")]
  public async Task<IActionResult> Put([FromBody] ActivityLogView activityLogView) {
    await _activityLogViewService.Update(activityLogView);

    return Ok();
  }
}