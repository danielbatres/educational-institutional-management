using System.Net.Http.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class RoleService : BaseService, IRoleService {
  
  public RoleService(HttpClient httpClient) : base(httpClient) {}

  public async Task Create(Role role) {
    var response = await HttpClient.PostAsJsonAsync("api/role/create", role);

    await CheckResponse(response);
  }
}

public interface IRoleService {
  Task Create(Role role); 
}