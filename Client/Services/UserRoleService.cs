using System.Net.Http.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class UserRoleService {
	public async Task Create(UserRole userRole) {
		var response = await HttpClient.PostAsJsonAsync("api/userrole/create", userRole);
		
		await CheckResponse(response);
	}
	
	public async Task Update(UserRole userRole) {
		var response = await HttpClient.PutAsJsonAsync("api/userrole/update", userRole);
		
		await CheckResponse(response);
	}
}

public interface IUserRoleService {
	Task Create(UserRole userRole);
	Task Update(UserRole userRole);
}