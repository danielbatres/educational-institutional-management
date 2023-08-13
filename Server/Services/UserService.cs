using edu_institutional_management.Shared.DTO;

namespace edu_institutional_management.Server.Services;

public class UserService : IUserService {
  private readonly MainContext context;

  public UserService(MainContext dbContext) {
    context = dbContext;
  }

  public IEnumerable<UserDto> Get() {
    return context.Users;
  }
}

public interface IUserService {
  IEnumerable<UserDto> Get();
}