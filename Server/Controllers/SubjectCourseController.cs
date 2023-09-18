using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectCourseController : ControllerBase {
  private readonly ISubjectCourseService _subjectCourseService;

  public SubjectCourseController(ISubjectCourseService subjectCourseService) {
    _subjectCourseService = subjectCourseService;
  }
  
  [HttpGet]
  public IActionResult Get(string courseId) {
    return Ok(_subjectCourseService.Get(Guid.Parse(courseId)));
  }
  
  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Post([FromBody] SubjectCourse subjectCourse) {
    await _subjectCourseService.Create(subjectCourse);
  
  return Ok();
  }
  
  [HttpDelete]
  [Route("remove")]
  public async Task<IActionResult> Delete(string subjectCourseId) {
    await _subjectCourseService.Delete(Guid.Parse(subjectCourseId));
    
    return Ok();
  }
}