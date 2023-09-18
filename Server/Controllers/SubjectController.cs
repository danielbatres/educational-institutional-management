using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectController : ControllerBase {
  private readonly ISubjectService _subjectService;
  
  public SubjectController(ISubjectService subjectService) {
    _subjectService = subjectService;
  }
  
  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Post([FromBody] Subject subject) {
    await _subjectService.Create(subject);
    
    return Ok();
  }
  
  [HttpPut]
  [Route("update")]
  public async Task<IActionResult> Put([FromBody] Subject subject) {
    await _subjectService.Update(subject);
    
    return Ok();
  }
}