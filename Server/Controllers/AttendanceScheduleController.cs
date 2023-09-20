using edu_institutional_management.Shared.Models;
using edu_institutional_management.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttendanceScheduleController : ControllerBase {
  private readonly IAttendanceScheduleService _attendanceScheduleService;
  
  public AttendanceScheduleController(IAttendanceScheduleService attendanceScheduleService) {
    _attendanceScheduleService = attendanceScheduleService;
  }
  
  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Post([FromBody] AttendanceSchedule attendanceSchedule) {
    await _attendanceScheduleService.Create(attendanceSchedule);
    
    return Ok();
  }
  
  [HttpPut]
  [Route("update")]
  public async Task<IActionResult> Put([FromBody] AttendanceSchedule attendanceSchedule) {
    await _attendanceScheduleService.Update(attendanceSchedule);
    
    return Ok();
  }
}