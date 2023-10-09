using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Containers;

public class CourseContext : BaseContainer {
  public Course Course { get; set; }
  public Guid CurrentCourseId { get; set; } 
  public ActionType CourseOption { get; set; }
  
  public void SetCourseOption(ActionType option) {
    CourseOption = option;

    NotifyStateChanged();
  }

  public void SetCurrentCourse(Guid id) {
    CurrentCourseId = id;

    NotifyStateChanged();
  }

  public void SetCourse(Course course) {
    Course = course;
    
    NotifyStateChanged();
  }
  
  public void SetNewCourse() {
    Course = new() {
      Id = Guid.NewGuid(),
      Name = string.Empty,
      GuideId = Guid.Empty,
      Acronym = string.Empty,
      StudentsCount = 0
    };
    
    NotifyStateChanged();
  }
}