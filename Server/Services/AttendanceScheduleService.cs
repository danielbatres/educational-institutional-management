using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class AttendanceScheduleService : BaseService, IAttendanceScheduleService{
  public AttendanceScheduleService(ApplicationContextService applicationContextService) : base(applicationContextService) {}

  public async Task Create(AttendanceSchedule attendanceSchedule) {
    _applicationContext.AttendanceSchedules.Add(attendanceSchedule);
    
    await _applicationContext.SaveChangesAsync();
  }
  
  public async Task Update(AttendanceSchedule attendanceSchedule) {
    var originalAttendanceSchedule = _applicationContext.AttendanceSchedules.FirstOrDefault(a => a.Id.Equals(attendanceSchedule.Id));

    if (originalAttendanceSchedule != null) {
      _applicationContext.Entry(originalAttendanceSchedule).State = EntityState.Detached;
      _applicationContext.Entry(attendanceSchedule).State = EntityState.Modified;

      await _applicationContext.SaveChangesAsync();
    }
  }
}

public interface IAttendanceScheduleService {
  Task Create(AttendanceSchedule attendanceSchedule);
  Task Update(AttendanceSchedule attendanceSchedule);
}