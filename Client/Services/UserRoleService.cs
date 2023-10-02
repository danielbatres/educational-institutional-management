using System.Net.Http.Json;
using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class UserRoleService : BaseService, IUserRoleService {
	private readonly UserRoleHubManager _userRoleHubManager;
	private readonly UserContext _userContext;

	public UserRoleService(HttpClient httpClient, UserRoleHubManager userRoleHubManager, UserContext userContext) : base(httpClient) {
		_userRoleHubManager = userRoleHubManager;
		_userContext = userContext;
	}
	
	public async Task Create(UserRole userRole) {
		var response = await HttpClient.PostAsJsonAsync("api/userrole/create", userRole);
		
		await CheckResponse(response);

		await SendUpdate();
	}
	
	public async Task Delete(Guid userId) {
		var response = await HttpClient.DeleteAsync($"api/userrole/remove?userId={userId}");
		
		await CheckResponse(response);

		await SendUpdate();
	}

	private async Task SendUpdate() {
		await _userRoleHubManager.SendUserRolesUpdatedAsync(_userContext.User.InstitutionId.ToString() ?? string.Empty);
	}
}

public interface IUserRoleService {
	Task Create(UserRole userRole);
  Task Delete(Guid userRoleId);
}