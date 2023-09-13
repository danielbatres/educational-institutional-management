using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class CourseService : BaseService, ICourseService {
  public CourseService(ApplicationContextService applicationContextService) : base (applicationContextService) {}
  
  public IEnumerable<Course> Get() {
    return _applicationContext.Courses.Include(c => c.AttendanceSchedules).Include(c => c.SubjectCourses);
  }
  
  public async Task Create(Course course) {
    _applicationContext.Courses.Add(course);
    
    await _applicationContext.SaveChangesAsync();
  }
  
  public async Task Update(Course course) {
    _applicationContext.Courses.Update(course);
    
    await _applicationContext.SaveChangesAsync();
  }
}

public interface ICourseService {
  Task Create(Course course);
  Task Update(Course course);
  IEnumerable<Course> Get();
}