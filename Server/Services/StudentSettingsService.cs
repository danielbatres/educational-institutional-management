using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class StudentSettingsService : BaseService, IStudentSettingsService {
  public StudentSettingsService(ApplicationContextService applicationContextService) : base(applicationContextService) {}
  
  public IEnumerable<StudentSettings> Get() {
    return _applicationContext.StudentSettings;
  }
  
  public async Task Update(StudentSettings studentSettings) {
    var originalStudentSettings = _applicationContext.StudentSettings.FirstOrDefault(s => s.Id == studentSettings.Id);
    
    if (originalStudentSettings != null) {
      _applicationContext.Entry(originalStudentSettings).State = EntityState.Detached;
      _applicationContext.Entry(studentSettings).State = EntityState.Modified;
      
      await _applicationContext.SaveChangesAsync();
    }
  }
}

public interface IStudentSettingsService {
  IEnumerable<StudentSettings> Get();
  Task Update(StudentSettings studentSettings);
}