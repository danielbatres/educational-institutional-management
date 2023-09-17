using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase {
  private readonly ICategoryService _categoryService;
  
  public CategoryController(ICategoryService categoryService) {
    _categoryService = categoryService;
  }

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

  [HttpDelete]
  [Route("remove")]
  public async Task<IActionResult> Delete(string categoryId) {
    await _categoryService.Delete(Guid.Parse(categoryId));
    
    return Ok();
  }
}