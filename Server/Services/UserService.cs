using edu_institutional_management.Client;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class UserService : IUserService {
  private readonly MainContext _context;

  public UserService(MainContext dbContext) {
    _context = dbContext;
  }

  public IEnumerable<User> Get() {
    return _context.Users;
  }

  public async Task Create(User user) {
    _context.Users.Add(user);

    await _context.SaveChangesAsync();
  }
}

public interface IUserService
{
  IEnumerable<User> Get();
  Task Create(User user);
}