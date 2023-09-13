using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase {
  private readonly ICategoryService _categoryService;
  
  [HttpGet]
  public IActionResult Get() {
    return Ok(_categoryService.Get());
  }
  
  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Post([FromBody] Category category) {
    await _categoryService.Create(category);
  
    return Ok();
  }
  
  [HttpPut]
  [Route("update")]
  public async Task<IActionResult> Put([FromBody] Category category) {
    await _categoryService.Update(category);
    
    return Ok();
  }
}