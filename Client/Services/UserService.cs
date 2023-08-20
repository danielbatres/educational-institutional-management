using System.Net.Http.Json;
using System.Text.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class UserService : BaseService, IUserService {
  public UserService(HttpClient httpClient) : base(httpClient) {}

  public async Task Register(User user) {
    var response = await HttpClient.PostAsJsonAsync("api/user/create", user);

    await CheckResponse(response);
  }

  public async Task<List<User>> GetUsers() {
    var response = await HttpClient.GetAsync("api/user");
    var content = await CheckResponseContent(response);

    return JsonSerializer.Deserialize<List<User>>(content, JsonOptions) ?? new();
  }
}

public interface IUserService {
  Task Register(User user);
  Task<List<User>> GetUsers();
}