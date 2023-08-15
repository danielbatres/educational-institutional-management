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

  public async Task Create(UserDto userDto) {
    context.Add(userDto);

    await context.SaveChangesAsync();
  }
}

public interface IUserService
{
  IEnumerable<UserDto> Get();
  Task Create(UserDto userDto);
}