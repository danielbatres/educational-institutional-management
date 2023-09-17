using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OptionController : ControllerBase {
  private readonly IOptionService _optionService;
  
  public OptionController(IOptionService optionService) {
    _optionService = optionService;
  }
  
  [HttpGet]
  public IActionResult Get(string fieldId) {
    return Ok(_optionService.Get(Guid.Parse(fieldId)));
  }
  
  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Post([FromBody] Option option) {
    await _optionService.Create(option);
    
    return Ok();
  }
  
  [HttpPut]
  [Route("update")]
  public async Task<IActionResult> Put([FromBody] Option option) {
    await _optionService.Update(option);
    
    return Ok();
  }
  
  [HttpDelete]
  [Route("remove")]
  public async Task<IActionResult> Delete(string optionId) {
    await _optionService.Delete(int.Parse(optionId));
    
    return Ok();
  }
}