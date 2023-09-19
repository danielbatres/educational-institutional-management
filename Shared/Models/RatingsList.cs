namespace edu_institutional_management.Shared.Models;

public class RatingsList {
  public Guid Id { get; set; }
  public string ListName { get ; set; }
  public string Description { get; set; }
  public ICollection<Rating>? Ratings { get; set; }
  public Guid SubjectCourseId { get; set; }
  public SubjectCourse? SubjectCourse { get; set; }
}