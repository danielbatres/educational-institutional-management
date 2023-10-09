namespace edu_institutional_management.Shared.Models;

public class Subject {
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string Color { get; set; }
  public ICollection<SubjectCourse>? SubjectCourses { get; set; }
}