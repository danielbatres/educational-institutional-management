using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class AttendanceScheduleService : BaseService, IAttendanceScheduleService{
  public AttendanceScheduleService(ApplicationContextService applicationContextService) : base(applicationContextService) {}

  public async Task Create(AttendanceSchedule attendanceSchedule) {
    _applicationContext.AttendanceSchedules.Add(attendanceSchedule);
    
    await _applicationContext.SaveChangesAsync();
  }
  
  public async Task Update(AttendanceSchedule attendanceSchedule) {
    _applicationContext.AttendanceSchedules.Update(attendanceSchedule);
    
    await _applicationContext.SaveChangesAsync();
  }
}

public interface IAttendanceScheduleService {
  Task Create(AttendanceSchedule attendanceSchedule);
  Task Update(AttendanceSchedule attendanceSchedule);
}