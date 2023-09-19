using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class CourseSchedule : Schedule {
  public Guid SubjectCourseId { get; set; }
  [JsonIgnore]
  public SubjectCourse? SubjectCourse { get; set; }
}