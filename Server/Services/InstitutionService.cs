using edu_institutional_management.Server.Models;

namespace edu_institutional_management.Server.Services;

public class InstitutionService : IInstitutionService
{
  private readonly CentralContext context;

  public InstitutionService(CentralContext dbContext)
  {
    context = dbContext;
  }

  public IEnumerable<Institution> Get()
  {
    return context.Institutions;
  }
}

public interface IInstitutionService
{
  IEnumerable<Institution> Get();
}