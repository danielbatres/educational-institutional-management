using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class SubjectService : BaseService, ISubjectService {
  public SubjectService(ApplicationContextService applicationContextService) : base (applicationContextService) {}
  
  public async Task Create(Subject subject) {
    _applicationContext.Subjects.Add(subject);
    
    await _applicationContext.SaveChangesAsync();
  }
  
  public async Task Update(Subject subject) {
    _applicationContext.Subjects.Update(subject);
    
    await _applicationContext.SaveChangesAsync();
  }
}

public interface ISubjectService {
  Task Create(Subject subject);
  Task Update(Subject subject);
}