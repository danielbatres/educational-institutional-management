using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

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

  public IEnumerable<Institution> Get() {
    return _context.Institutions;
  }

  public IEnumerable<User> GetInstitutionUsers(Guid institutionId) {
    return _context.Users.Where(u => u.InstitutionId == institutionId).Include(u => u.OnlineStatus);
  }
}

public interface IInstitutionService {
  Task Create(Institution institution);
  IEnumerable<Institution> Get();
  IEnumerable<User> GetInstitutionUsers(Guid institutionId);
}