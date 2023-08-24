using System.Net.Http.Json;
using System.Text.Json;
using edu_institutional_management.Shared.Models;
using edu_institutional_management.Client.Containers;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Services;

public class UserService : BaseService, IUserService {
  private UserContext UserContext { get; set; }
  private NavigationManager NavigationManager { get; set; }

  public UserService(HttpClient httpClient, UserContext userContext, NavigationManager navigationManager) : base(httpClient) {
    UserContext = userContext;
    NavigationManager = navigationManager;
  }

  public async Task Register(User user) {
    var response = await HttpClient.PostAsJsonAsync("api/user/create", user);

    await CheckResponse(response);
  }

  public async Task<List<User>> GetUsers() {
    var response = await HttpClient.GetAsync("api/user");
    var content = await CheckResponseContent(response);

    return JsonSerializer.Deserialize<List<User>>(content, JsonOptions) ?? new();
  }

  public async Task<bool> LoginUser(User user) {
    List<User> Users = await GetUsers();

    if (Users.Count != 0) {
      Users = Users.Where(x => x.Register.Email.Equals(user.Register.Email) && x.Register.Password.Equals(user.Register.Password)).ToList();

      if (Users.Count == 1) {
        UserContext.User = Users[0];

        UserContext.SetIsActiveUser(true);
        return true;
      }
    }

    return false;
  }
}

public interface IUserService {
  Task Register(User user);
  Task<List<User>> GetUsers();
  Task<bool> LoginUser(User user);
}