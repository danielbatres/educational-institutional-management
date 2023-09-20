using edu_institutional_management.Shared.Models;
using edu_institutional_management.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticController : ControllerBase {
	private readonly IStatisticService _statisticService;
	
	public StatisticController(IStatisticService statisticService) {
		_statisticService = statisticService;
	}
	
	[HttpGet]
	public IActionResult Get() {
		return Ok(_statisticService.Get());
	}
	
	[HttpPost]
	[Route("create")]
	public async Task<IActionResult> Post([FromBody] Statistic statistic) {
		await _statisticService.Create(statistic);
		
		return Ok();
	}
	
	[HttpPut]
	[Route("update")]
	public async Task<IActionResult> Put([FromBody] Statistic statistic) {
		await _statisticService.Update(statistic);
		
		return Ok();
	}
	
	[HttpDelete]
	[Route("remove")]
	public async Task<IActionResult> Delete(string statisticId) {
		await _statisticService.Delete(Guid.Parse(statisticId));
		
		return Ok();
	}
}