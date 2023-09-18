using edu_institutional_management.Shared.Models;
using edu_institutional_management.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FieldInformationController : ControllerBase {
  private readonly IFieldInformationService _fieldInformationService;

  public FieldInformationController(IFieldInformationService fieldInformationService) {
    _fieldInformationService = fieldInformationService;
  }

  [HttpGet]
  public IActionResult Get(string studentId) {
    return Ok(_fieldInformationService.Get(Guid.Parse(studentId)));
  }

  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Post([FromBody] FieldInformation fieldInformation) {
    await _fieldInformationService.Create(fieldInformation);
    
    return Ok();
  }
  
  [HttpPut]
  [Route("update")]
  public async Task<IActionResult> Put([FromBody] FieldInformation fieldInformation) {
    await _fieldInformationService.Update(fieldInformation);

    return Ok();
  }
}