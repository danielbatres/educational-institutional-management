using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class AttendanceService : BaseService, IAttendanceService {
  public AttendanceService(ApplicationContextService applicationContextService) : base(applicationContextService) {}
  
  public async Task Create(Attendance attendance) {
    _applicationContext.Attendances.Add(attendance);
    
    await _applicationContext.SaveChangesAsync();
  }
  
  public async Task Update(Attendance attendance) {
    var originalAttendance = _applicationContext.Attendances.FirstOrDefault(a => a.Id.Equals(attendance.Id));

    if (originalAttendance != null) {
      _applicationContext.Entry(originalAttendance).State = EntityState.Detached;
      _applicationContext.Entry(attendance).State = EntityState.Modified;

      await _applicationContext.SaveChangesAsync();
    }
  }
}

public interface IAttendanceService {
  Task Create(Attendance attendance);
  Task Update(Attendance attendance);
}