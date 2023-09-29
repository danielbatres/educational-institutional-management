using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GeneralNotificationController : ControllerBase {
  private readonly IGeneralNotificationService _generalNotificationService;

  public GeneralNotificationController(IGeneralNotificationService generalNotificationService) {
    _generalNotificationService = generalNotificationService;
  }

  [HttpGet]
  public IActionResult Get() {
    return Ok(_generalNotificationService.Get());
  }

  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Create([FromBody] GeneralNotification generalNotification) {
    await _generalNotificationService.Create(generalNotification);

    return Ok();
  }

  [HttpPut]
  [Route("update")]
  public async Task<IActionResult> Update([FromBody] GeneralNotification generalNotification) {
    await _generalNotificationService.Update(generalNotification);

    return Ok();
  }
}