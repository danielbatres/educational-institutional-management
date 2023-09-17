using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentSettingsController : ControllerBase {
  private readonly IStudentSettingsService _studentSettingsService;
  
  public StudentSettingsController(IStudentSettingsService studentSettingsService) {
    _studentSettingsService = studentSettingsService;
  }
  
  [HttpGet]
  public IActionResult Get() {
    return Ok(_studentSettingsService.Get());
  }
  
  [HttpPut]
  [Route("update")]
  public async Task<IActionResult> Put([FromBody] StudentSettings studentSettings) {
    await _studentSettingsService.Update(studentSettings);
    
    return Ok();
  }
}