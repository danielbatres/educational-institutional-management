using System.Security.Cryptography;
using System.Text;
using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;


namespace edu_institutional_management.Server.Services;

public class UserService : IUserService {
  private readonly MainContext _context;

  public UserService(MainContext dbContext) {
    _context = dbContext;
  }

  public User LoginUser(User user) {
    User loggedInUser = _context.Users.Include(u => u.Register).Include(u => u.OnlineStatus).Where(x => x.Register.Email.Equals(user.Register.Email) && VerifyPassword(x.Register.Password, user.Register.Password)).ToList()[0];

    return loggedInUser ?? new User();
  }

  public IEnumerable<User> Get() {
    return _context.Users.Include(u => u.Register).Include(u => u.OnlineStatus);
  }

  public async Task Create(User user) {
    _context.Users.Add(user);

    await _context.SaveChangesAsync();
  }

  public async Task Update(User user) {
    _context.Users.Update(user);

    await _context.SaveChangesAsync();
  }

  private bool VerifyPassword(string storedPassword, string passwordAttempt)
  {
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

public interface IUserService
{
  User LoginUser(User user);
  IEnumerable<User> Get();
  Task Create(User user);
  Task Update(User user);
}