using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class CourseScheduleService : BaseService, ICourseScheduleService {
  public CourseScheduleService(ApplicationContextService applicationContextService) : base (applicationContextService) { }
  
  public IEnumerable<CourseSchedule> Get(Guid subjectCourseId) {
    return _applicationContext.CourseSchedules.Where(c => c.SubjectCourseId.Equals(subjectCourseId));
  }
  
  public async Task Create(CourseSchedule courseSchedule) {
    _applicationContext.CourseSchedules.Add(courseSchedule);
    
    await _applicationContext.SaveChangesAsync();
  }
  
  public async Task Delete(Guid courseScheduleId) {
    var originalCourseSchedule = _applicationContext.CourseSchedules.FirstOrDefault(c => c.Id.Equals(courseScheduleId));
    
    if (originalCourseSchedule != null) {
      _applicationContext.CourseSchedules.Remove(originalCourseSchedule);
      
      await _applicationContext.SaveChangesAsync();
    }
  }
}

public interface ICourseScheduleService {
  IEnumerable<CourseSchedule> Get(Guid subjectCourseId);
  Task Create(CourseSchedule courseSchedule);
  Task Delete(Guid courseScheduleId);
}