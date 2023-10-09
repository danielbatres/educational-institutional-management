using System.Text.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class PermissionService : BaseService, IPermissionService {
  public PermissionService(HttpClient httpClient) : base(httpClient) {}

  public async Task<List<Permission>> Get() {
    var response = await HttpClient.GetAsync("api/permission");

    var content = await CheckResponseContent(response);

    return JsonSerializer.Deserialize<List<Permission>>(content, JsonOptions) ?? new();
  }
}

public interface IPermissionService {
  Task<List<Permission>> Get();
}