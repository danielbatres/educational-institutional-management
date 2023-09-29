using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace edu_institutional_management.Server.Hubs;

public class EventsHub : MainHub {
	public async Task SendEventsUpdate(string groupName) {
		await Clients.Group(groupName).SendAsync("EventsUpdated");
	}
}