using System.Net.Http.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class AttendanceService : BaseService, IAttendanceService {
	public AttendanceService(HttpClient httpClient) : base (httpClient) {}
	
	public async Task Create(Attendance attendance) {
		var response = await HttpClient.PostAsJsonAsync("api/attendance/create", attendance);
		
		await CheckResponse(response);
	}
	
	public async Task Update(Attendance attendance) {
		var response = await HttpClient.PutAsJsonAsync("api/attendance/update", attendance);
		
		await CheckResponse(response);
	}
}

public interface IAttendanceService {
	Task Create(Attendance attendance);
	Task Update(Attendance attendance);
}