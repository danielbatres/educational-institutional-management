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

  public User? LoginUser(User user) {
    var loggedInUser = _context.Users
        .Include(u => u.Register)
        .Include(u => u.OnlineStatus)
        .FirstOrDefault(x => x.Register.Email.Equals(user.Register.Email));

    //if (loggedInUser != null && VerifyPassword(loggedInUser.Register.Password, user.Register.Password)) {
      //return loggedInUser;
    //}

    return null;
  }

  public IEnumerable<User> Get() {
    return _context.Users.Include(u => u.Register).Include(u => u.OnlineStatus).Include(u => u.Institution).Include(u => u.Payment);
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
  User? LoginUser(User user);
  IEnumerable<User> Get();
  Task Create(User user);
  Task Update(User user);
}