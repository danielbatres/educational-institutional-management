using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase {
  private readonly IStudentService _studentService;

  public StudentController(IStudentService studentService) {
    _studentService = studentService;
  }
  
  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Post([FromBody] Student student) {
    await _studentService.Create(student);
  
  return Ok();
  }
  
  [HttpPut]
  [Route("update")]
  public async Task<IActionResult> Put([FromBody] Student student) {
    await _studentService.Update(student);
  
  return Ok();
  }
  
  [HttpGet]
  public IActionResult Get() {
    return Ok(_studentService.Get());
  }
}