using edu_institutional_management.Shared.Models;
using edu_institutional_management.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationVisualizerController : ControllerBase {
	private readonly INotificationVisualizerService _notificationVisualizerService;
	
	public NotificationVisualizerController(INotificationVisualizerService notificationVisualizerService) {
		_notificationVisualizerService = notificationVisualizerService;
	}
	
	[HttpGet]
	public IActionResult Get(string userId) {
		return Ok(_notificationVisualizerService.Get(Guid.Parse(userId)));
	}
	
	[HttpPost]
	[Route("create")]
	public async Task<IActionResult> Post([FromBody] NotificationVisualizer notificationVisualizer) {
		await _notificationVisualizerService.Create(notificationVisualizer);
		
		return Ok();
	}
	
	[HttpPut]
	[Route("update")]
	public async Task<IActionResult> Put([FromBody] NotificationVisualizer notificationVisualizer) {
		await _notificationVisualizerService.Update(notificationVisualizer);
		
		return Ok();
	}
}