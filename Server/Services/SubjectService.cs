using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class SubjectService : BaseService, ISubjectService {
  public SubjectService(ApplicationContextService applicationContextService) : base (applicationContextService) {}
  
  public IEnumerable<Subject> Get() {
    return _applicationContext.Subjects;
  }

  public async Task Create(Subject subject) {
    _applicationContext.Subjects.Add(subject);
    
    await _applicationContext.SaveChangesAsync();
  }
  
  public async Task Update(Subject subject) {
    var originalSubject = _applicationContext.Subjects.FirstOrDefault(s => s.Id.Equals(subject.Id));
    
    if (originalSubject != null) {
      _applicationContext.Entry(originalSubject).State = EntityState.Detached;
      _applicationContext.Entry(subject).State = EntityState.Modified;

      await _applicationContext.SaveChangesAsync();
    }
  }
}

public interface ISubjectService {
  IEnumerable<Subject> Get();
  Task Create(Subject subject);
  Task Update(Subject subject);
}