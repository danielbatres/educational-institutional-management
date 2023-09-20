using System.Net.Http.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class AttendanceScheduleService : BaseService, IAttendanceScheduleService {
	public AttendanceScheduleService(HttpClient httpClient) : base(httpClient) {}
	
	public async Task Create(AttendanceSchedule attendanceSchedule) {
		var response = await HttpClient.PostAsJsonAsync("api/attendanceschedule/create", attendanceSchedule);
		
		await CheckResponse(response);
	}
	
	public async Task Update(AttendanceSchedule attendanceSchedule) {
		var response = await HttpClient.PutAsJsonAsync("api/attendanceschedule/update", attendanceSchedule);
		
		await CheckResponse(response);
	}
}

public interface IAttendanceScheduleService {
	Task Create(AttendanceSchedule attendanceSchedule);
	Task Update(AttendanceSchedule attendanceSchedule);
}