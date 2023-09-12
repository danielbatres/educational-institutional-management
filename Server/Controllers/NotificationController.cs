using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase {
  private readonly INotificationService _notificationService;

  public NotificationController(INotificationService notificationService) {
    _notificationService = notificationService;
  }

  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Create([FromBody] Notification notification) {
    await _notificationService.Create(notification);

    return Ok();
  }

  [HttpPut]
  [Route("update")]
  public async Task<IActionResult> Update([FromBody] Notification notification) {
    await _notificationService.Update(notification);

    return Ok();
  }
}