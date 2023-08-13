using edu_institutional_management.Shared.DTO;

namespace edu_institutional_management.Server.Services;

public class InstitutionService : IInstitutionService {
  private readonly MainContext context;

  public InstitutionService(MainContext dbContext)
  {
    context = dbContext;
  }

  public IEnumerable<InstitutionDto> Get()
  {
    return context.Institutions;
  }
}

public interface IInstitutionService {
  IEnumerable<InstitutionDto> Get();
}