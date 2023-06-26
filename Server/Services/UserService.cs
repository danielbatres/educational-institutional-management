using edu_institutional_management.Server.Models;

namespace edu_institutional_management.Server.Services;

public class UserService : IUserService {
  private readonly CentralContext context;

  public UserService(CentralContext dbContext) {
    context = dbContext;
  }

  public IEnumerable<User> Get() {
    return context.Users;
  }
}

public interface IUserService {
  IEnumerable<User> Get();
}