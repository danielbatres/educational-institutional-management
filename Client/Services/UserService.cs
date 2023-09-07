using System.Net.Http.Json;
using System.Text.Json;
using edu_institutional_management.Shared.Models;
using edu_institutional_management.Client.Containers;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;

namespace edu_institutional_management.Client.Services;

public class UserService : BaseService, IUserService {
  private UserContext UserContext { get; set; }

  public UserService(HttpClient httpClient, UserContext userContext) : base(httpClient) {
    UserContext = userContext;
  }

  public async Task Register(User user) {
    user.Register.Password = HashPassword(user.Register.Password);
    user.UserName = "newUser" + Guid.NewGuid();

    var response = await HttpClient.PostAsJsonAsync("api/user/create", user);

    await CheckResponse(response);
  }

  public async Task Update(User user) {
    if (user != null) {
      var response = await HttpClient.PutAsJsonAsync("api/user/update", user);

      await CheckResponse(response);

      UserContext.SetUser(user);
    }
  }

  public async Task<List<User>> GetUsers() {
    var response = await HttpClient.GetAsync("api/user");
    var content = await CheckResponseContent(response);

    return JsonSerializer.Deserialize<List<User>>(content, JsonOptions) ?? new();
  }

  public async Task<List<List<object>>> LoginUser(User user) {
    List<User> Users = await GetUsers();
    List<List<object>> LoginInfo = new() { 
      new List<object> { "Correo electrónico invalido", false }, 
      new List<object> { "Contraseña incorrecta", false } 
    };

    void SetLoginInfo(int index) {
      LoginInfo[index][0] = string.Empty;
      LoginInfo[index][1] = true;
    }

    if (Users.Count != 0) {
      foreach (var userItem in Users) {
        if (userItem.Register.Email.Equals(user.Register.Email)) {
          SetLoginInfo(0);

          if (VerifyPassword(userItem.Register.Password, user.Register.Password)) {
            SetLoginInfo(1);
          }
        }
      }

      Users = Users.Where(x => x.Register.Email.Equals(user.Register.Email) && VerifyPassword(x.Register.Password, user.Register.Password)).ToList();

      if (Users.Count == 1) {
        var response = await HttpClient.PostAsJsonAsync("api/user/login-user", Users[0]);
        //var content = await CheckResponseContent(response);

        //UserContext.SetUser(JsonSerializer.Deserialize<User>(content, JsonOptions) ?? new());
        UserContext.SetUser(Users[0]);

        UserContext.SetIsActiveUser(true);
      }
    }

    return LoginInfo;
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
  Task Update(User user);
  Task<List<User>> GetUsers();
  Task<List<List<object>>> LoginUser(User user);
}