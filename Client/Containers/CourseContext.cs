using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Containers;

public class CourseContext : BaseContainer {
  public Course Course { get; set; }
  
  public void SetCourse(Course course) {
    Course = course;
    
    NotifyStateChanged();
  }
  
  public void SetNewCourse() {
    Course = new() {
      Id = Guid.NewGuid(),
      Name = string.Empty,
      Guide = string.Empty,
      Acronym = string.Empty,
      StudentsCount = 0
    };
    
    NotifyStateChanged();
  }
}