using edu_institutional_management.Shared.Models;
using edu_institutional_management.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FieldController : ControllerBase {
  private readonly IFieldService _fieldService;
  
  public FieldController(IFieldService fieldService) {
    _fieldService = fieldService;
  }
  
  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Post([FromBody] Field field) {
    await _fieldService.Create(field);
    
    return Ok();
  }
  
  [HttpPut]
  [Route("update")]
  public async Task<IActionResult> Put([FromBody] Field field) {
    await _fieldService.Update(field);
    
    return Ok();
  }
  
  [HttpDelete]
  [Route("remove")]
  public async Task<IActionResult> Delete(string fieldId) {
    await _fieldService.Delete(Guid.Parse(fieldId));

    return Ok();
  }
}