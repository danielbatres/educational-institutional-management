using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase {
  private readonly ICourseService _courseService;
  
  public CourseController(ICourseService courseService) {
    _courseService = courseService;
  }
  
  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Post([FromBody] Course course) {
    await _courseService.Create(course);
    
    return Ok();
  }
  
  [HttpPut]
  [Route("update")]
  public async Task<IActionResult> Put([FromBody] Course course) {
    await _courseService.Update(course);
    
    return Ok();
  }
  
  [HttpGet]
  public IActionResult Get() {
    return Ok(_courseService.Get());
  }
}