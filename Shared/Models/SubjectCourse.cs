using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class SubjectCourse {
  public Guid Id { get; set; }
  public Guid CourseId { get; set; }
  [JsonIgnore]
  public Course? Course { get; set; }
  public Guid SubjectId { get; set; }
  public Guid? UserId { get; set; }
  [JsonIgnore]
  public Subject? Subject { get; set; }
  public ICollection<RatingsList>? RatingsLists { get; set; }
  public ICollection<CourseSchedule>? CourseSchedules { get; set; }
}