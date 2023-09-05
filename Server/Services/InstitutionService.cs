using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class InstitutionService : IInstitutionService {
  private readonly MainContext _context;

  public InstitutionService(MainContext dbContext)
  {
    _context = dbContext;
  }

  public async Task Create(Institution institution) {
    _context.Institutions.Add(institution);

    await _context.SaveChangesAsync();
  }
}

public interface IInstitutionService {
  Task Create(Institution institution);
}