using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class RegisterService : IRegisterService
{
  private readonly MainContext context;

  public RegisterService(MainContext dbContext)
  {
    context = dbContext;
  }

  public IEnumerable<Register> Get()
  {
    return context.Registers;
  }

  public async Task Create(Register register)
  {
    context.Add(register);

    await context.SaveChangesAsync();
  }
}

public interface IRegisterService
{
  IEnumerable<Register> Get();
  Task Create(Register register);
}