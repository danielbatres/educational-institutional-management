using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SettingsController : ControllerBase {
  private readonly ISettingsService _settingsService;

  public SettingsController(ISettingsService settingsService) {
    _settingsService = settingsService;
  }

  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Create([FromBody] Settings settings) {
    await _settingsService.Create(settings);

    return Ok();
  }

  [HttpPut]
  [Route("update")]
  public async Task<IActionResult> Update([FromBody] Settings settings) {
    await _settingsService.Update(settings);

    return Ok();
  }

  [HttpGet]
  [Route("get-by-user-id")]
  public ActionResult<Settings> GetSettingsByUserId(Guid userId) {
    Settings Settings = _settingsService.GetSettingsByUserId(userId);

    if (Settings == null) return NotFound();

    return Ok(Settings);
  }
}