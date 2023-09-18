using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class SubjectCourseService : BaseService, ISubjectCourseService {
  public SubjectCourseService(ApplicationContextService applicationContextService) : base(applicationContextService) {}

  public IEnumerable<SubjectCourse> Get(Guid courseId) {
    return _applicationContext.SubjectCourses.Where(sc => sc.CourseId.Equals(courseId));
  }

  public async Task Create(SubjectCourse subjectCourse) {
    _applicationContext.SubjectCourses.Add(subjectCourse);
    
    await _applicationContext.SaveChangesAsync();
  }
  
  public async Task Delete(Guid subjectCourseId) {
    var originalSubjectCourse = _applicationContext.SubjectCourses.FirstOrDefault(sc => sc.Id.Equals(subjectCourseId));

    if (originalSubjectCourse != null) {
      _applicationContext.SubjectCourses.Remove(originalSubjectCourse);

      await _applicationContext.SaveChangesAsync();
    }
  }
}

public interface ISubjectCourseService {
  IEnumerable<SubjectCourse> Get(Guid courseId);
  Task Create(SubjectCourse subjectCourse);
  Task Delete(Guid subjectCourseId);
}