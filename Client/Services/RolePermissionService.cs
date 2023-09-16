using System.Net.Http.Json;
using System.Text.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class RolePermissionService : BaseService, IRolePermissionService {
  public RolePermissionService(HttpClient httpClient) : base(httpClient) { }

  public async Task<List<RolePermission>> Get(Guid roleId) {
    var response = await HttpClient.GetAsync($"api/rolepermission?roleId={roleId}");

    var content = await CheckResponseContent(response);

    return JsonSerializer.Deserialize<List<RolePermission>>(content, JsonOptions) ?? new();
  }

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
  Task<List<RolePermission>> Get(Guid roleId);
}