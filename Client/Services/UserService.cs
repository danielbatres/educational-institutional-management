using System.Net.Http.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class UserService : BaseService, IUserService {
  public UserService(HttpClient httpClient) : base(httpClient) {}

  public async Task Register(User user) {
    var response = await HttpClient.PostAsJsonAsync("api/user/create", user);

    var content = await response.Content.ReadAsStringAsync();

    if (!response.IsSuccessStatusCode)
    {
      throw new ApplicationException(content);
    }
  }
}

public interface IUserService {
  Task Register(User user);
}