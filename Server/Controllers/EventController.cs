using edu_institutional_management.Shared.Models;
using edu_institutional_management.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

public class EventController : ControllerBase {
	private readonly IEventService _eventService;
	
	public EventController(IEventService eventService) {
		_eventService = eventService;
	}
	
	[HttpGet]
	public IActionResult Get() {
		return Ok(_eventService.Get());
	}
	
	[HttpPost]
	[Route("create")]
	public async Task<IActionResult> Post([FromBody] Event eventModel) {
		await _eventService.Create(eventModel);
		
		return Ok();
	}
	
	[HttpPut]
	[Route("update")]
	public async Task<IActionResult> Put([FromBody] Event eventModel) {
		await _eventService.Update(eventModel);
		
		return Ok();
	}
	
	[HttpDelete]
	[Route("remove")]
	public async Task<IActionResult> Delete(string eventId) {
		await _eventService.Delete(Guid.Parse(eventId));
		
		return Ok();
	}
}