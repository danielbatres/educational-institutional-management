using System.Net.Http.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class RolePermissionService : BaseService, IRolePermissionService {
  public RolePermissionService(HttpClient httpClient) : base(httpClient) { }

  public async Task Create(RolePermission rolePermission) {
    var response = await HttpClient.PostAsJsonAsync("api/rolepermission/create", rolePermission);

    await CheckResponse(response);
  }

  public async Task Delete(int rolePermissionId) {
    var response = await HttpClient.DeleteAsync($"api/rolepermission/remove?rolePermissionId={rolePermissionId}");
    await CheckResponse(response);
  }
}

public interface IRolePermissionService {
  Task Create(RolePermission rolePermission);
  Task Delete(int rolePermission);
}