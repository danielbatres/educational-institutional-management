using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class AttendanceService : BaseService, IAttendanceService {
  public AttendanceService(ApplicationContextService applicationContextService) : base(applicationContextService) {}
  
  public async Task Create(Attendance attendance) {
    _applicationContext.Attendances.Add(attendance);
    
    await _applicationContext.SaveChangesAsync();
  }
  
  public async Task Update(Attendance attendance) {
    _applicationContext.Attendances.Update(attendance);
    
    await _applicationContext.SaveChangesAsync();
  }
}

public interface IAttendanceService {
  Task Create(Attendance attendance);
  Task Update(Attendance attendance);
}