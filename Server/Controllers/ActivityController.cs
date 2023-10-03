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

  [HttpPost]
  [Route("create-activity-example")]
  public async Task<IActionResult> Create() {
    for (int i = 0; i < 59; i++) {
      await CreateActivity(2, i);

      if (i == 4 || i == 10 || i == 3 || i == 6 || i == 17) {
        await CreateActivity(i, i);
      }

      if (i == 10 || i == 24 || i == 30 || i == 26 || i == 22 || i == 48 || i == 20 || i == 38) {
        await CreateActivity(i / 2, i);
      }
    }
    
    return Ok();
  }

  private async Task CreateActivity(int times, int index) {
    for (int i = 0; i < times; i++) {
      await _activityService.Create(new() {
        ActionType = ActionType.Create,
        Author = "John Doe",
        Title = "Activity example",
        Message = "Activity example",
        UserName = "John Doe",
        Date = DateTime.Now.AddDays(-index)
      });
    }
  }
}