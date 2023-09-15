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

  public async Task Update(Institution institution) {
    _context.Institutions.Update(institution);
    
    await _context.SaveChangesAsync();
  }

  public IEnumerable<Institution> Get() {
    return _context.Institutions;
  }

  public IEnumerable<User> GetInstitutionUsers(Guid institutionId) {
    return _context.Users.Where(u => u.InstitutionId == institutionId).Include(u => u.OnlineStatus);
  }

  public Institution GetInstitution(Guid institutionId) {
    return _context.Institutions.Where(i => i.Id == institutionId).FirstOrDefault();
  }
}

public interface IInstitutionService {
  Task Create(Institution institution);
  Task Update(Institution institution);
  IEnumerable<Institution> Get();
  IEnumerable<User> GetInstitutionUsers(Guid institutionId);
  Institution GetInstitution(Guid institutionId);
}