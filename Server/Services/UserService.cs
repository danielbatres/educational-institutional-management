using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;


namespace edu_institutional_management.Server.Services;

public class UserService : IUserService {
  private readonly MainContext _context;

  public UserService(MainContext dbContext) {
    _context = dbContext;
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
}

public interface IUserService
{
  IEnumerable<User> Get();
  Task Create(User user);
  Task Update(User user);
}