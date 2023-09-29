using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RatingsListController : ControllerBase {
  private readonly IRatingsListService _ratingsListService;

  public RatingsListController(IRatingsListService ratingsListService) {
    _ratingsListService = ratingsListService;
  }

  [HttpGet]
  public IActionResult Get() {
    return Ok(_ratingsListService.Get());
  }

  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Post([FromBody] RatingsList ratingsList) {
    await _ratingsListService.Create(ratingsList);

    return Ok();
  }

  [HttpPut]
  [Route("update")]
  public async Task<IActionResult> Put([FromBody] RatingsList ratingsList) {
    await _ratingsListService.Update(ratingsList);

    return Ok();
  }

  [HttpDelete]
  [Route("remove")]
  public async Task<IActionResult> Delete(string ratingsListId) {
    await _ratingsListService.Delete(Guid.Parse(ratingsListId));

    return Ok();
  }
}