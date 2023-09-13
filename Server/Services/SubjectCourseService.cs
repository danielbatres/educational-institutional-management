using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class SubjectCourseService : BaseService {
  public SubjectCourseService(ApplicationContextService applicationContextService) : base(applicationContextService) {}

  public async Task Create(SubjectCourse subjectCourse) {
    _applicationContext.SubjectCourses.Add(subjectCourse);
    
    await _applicationContext.SaveChangesAsync();
  }
  
  public async Task Update(SubjectCourse subjectCourse) {
    _applicationContext.SubjectCourses.Add(subjectCourse);
    
    await _applicationContext.SaveChangesAsync();
  }
}

public interface ISubjectCourseService {
  Task Create(SubjectCourse subjectCourse);
  Task Update(SubjectCourse subjectCourse);
}