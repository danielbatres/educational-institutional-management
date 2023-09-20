using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace edu_institutional_management.Server.Hubs;

public class AttendanceScheduleHub : MainHub {
	public async Task SendAttendanceScheduleUpdate(string groupName) {
		await Clients.Groups(groupName).SendAsync("AttendanceScheduleUpdated");
	}
}