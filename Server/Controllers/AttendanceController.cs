using edu_institutional_management.Shared.Models;
using edu_institutional_management.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttendanceController : ControllerBase {
  private readonly IAttendanceService _attendanceService;
  
  public AttendanceController(IAttendanceService attendanceService) {
    _attendanceService = attendanceService;
  }
  
  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Post([FromBody] Attendance attendance) {
    await _attendanceService.Create(attendance);
    
    return Ok();
  }
  
  [HttpPut]
  [Route("update")]
  public async Task<IActionResult> Put([FromBody] Attendance attendance) {
    await _attendanceService.Update(attendance);
    
    return Ok();
  }
}