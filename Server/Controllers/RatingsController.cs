using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RatingsController : ControllerBase {
  private readonly IRatingService _ratingService;

  public RatingsController(IRatingService ratingService) {
    _ratingService = ratingService;
  }

  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Post([FromBody] Rating rating) {
    await _ratingService.Create(rating);

    return Ok();
  }

  [HttpPut]
  [Route("update")]
  public async Task<IActionResult> Put([FromBody] Rating rating) {
    await _ratingService.Update(rating);

    return Ok();
  }
}