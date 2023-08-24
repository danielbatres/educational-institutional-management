using System.Net.Http.Json;
using System.Text.Json;
using edu_institutional_management.Shared.Models;
using edu_institutional_management.Client.Containers;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.Components;
using System.Security.Cryptography;
using System.Text;

namespace edu_institutional_management.Client.Services;

public class UserService : BaseService, IUserService {
  private UserContext UserContext { get; set; }
  private NavigationManager NavigationManager { get; set; }

  public UserService(HttpClient httpClient, UserContext userContext, NavigationManager navigationManager) : base(httpClient) {
    UserContext = userContext;
    NavigationManager = navigationManager;
  }

  public async Task Register(User user) {
    user.Register.Password = HashPassword(user.Register.Password);

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
      Users = Users.Where(x => x.Register.Email.Equals(user.Register.Email) && VerifyPassword(x.Register.Password, user.Register.Password)).ToList();

      if (Users.Count == 1) {
        UserContext.User = Users[0];

        UserContext.SetIsActiveUser(true);
        return true;
      }
    }

    return false;
  }

  private string HashPassword(string password) {
    using var sha256 = SHA256.Create();
    byte[] saltBytes = new byte[16];
    using (var rng = RandomNumberGenerator.Create()) {
        rng.GetBytes(saltBytes);
    }

    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
    byte[] combinedBytes = new byte[passwordBytes.Length + saltBytes.Length];
    Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
    Buffer.BlockCopy(saltBytes, 0, combinedBytes, passwordBytes.Length, saltBytes.Length);
    byte[] hashedBytes = sha256.ComputeHash(combinedBytes);

    string hashedPassword = Convert.ToBase64String(hashedBytes);
    string salt = Convert.ToBase64String(saltBytes);

    return $"{hashedPassword}:{salt}";
  }

  private bool VerifyPassword(string storedPassword, string passwordAttempt) {
    string[] parts = storedPassword.Split(':');
    if (parts.Length != 2)
      return false;

    byte[] saltBytes = Convert.FromBase64String(parts[1]);
    byte[] passwordAttemptBytes = Encoding.UTF8.GetBytes(passwordAttempt);
    byte[] combinedBytes = new byte[passwordAttemptBytes.Length + saltBytes.Length];
    Buffer.BlockCopy(passwordAttemptBytes, 0, combinedBytes, 0, passwordAttemptBytes.Length);
    Buffer.BlockCopy(saltBytes, 0, combinedBytes, passwordAttemptBytes.Length, saltBytes.Length);

    using var sha256 = SHA256.Create();
    byte[] hashedAttemptBytes = sha256.ComputeHash(combinedBytes);
    string hashedAttempt = Convert.ToBase64String(hashedAttemptBytes);

    return parts[0] == hashedAttempt;
  }
}

public interface IUserService {
  Task Register(User user);
  Task<List<User>> GetUsers();
  Task<bool> LoginUser(User user);
}