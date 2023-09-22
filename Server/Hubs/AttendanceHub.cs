using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace edu_institutional_management.Server.Hubs;

public class AttendanceHub : MainHub {
	private readonly IAttendanceService _attendanceService;

	public AttendanceHub(IAttendanceService attendanceService) {
		_attendanceService = attendanceService;
	}
	
	public async Task SendAttendanceUpdate(string groupName) {
		await Clients.Groups(groupName).SendAsync("AttendanceUpdated");
	}
}