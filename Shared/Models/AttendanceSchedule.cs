using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class AttendanceSchedule : Schedule {
  public Guid CourseId { get; set; }
  [JsonIgnore]
  public Course Course { get; set; }
}