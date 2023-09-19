using edu_institutional_management.Shared.Models;
using edu_institutional_management.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseScheduleController : ControllerBase {
  private readonly ICourseScheduleService _courseScheduleService;

  public CourseScheduleController(ICourseScheduleService courseScheduleService) {
    _courseScheduleService = courseScheduleService;
  }
  
  [HttpGet]
  public IActionResult Get(string subjectCourseId) {
    return Ok(_courseScheduleService.Get(Guid.Parse(subjectCourseId)));
  }
  
  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Post([FromBody] CourseSchedule courseSchedule) {
    await _courseScheduleService.Create(courseSchedule);
    
    return Ok();
  }
  
  [HttpPut]
  [Route("remove")]
  public async Task<IActionResult> Delete(string courseScheduleId) {
    await _courseScheduleService.Delete(Guid.Parse(courseScheduleId));
    
    return Ok();
  }
}