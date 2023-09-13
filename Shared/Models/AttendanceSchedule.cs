using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class AttendanceSchedule {
  public Guid Id { get; set; }
  public DayOfWeek DayOfWeek { get; set; }
  public TimeSpan StarTime { get; set; }
  public TimeSpan EndTime { get; set; }
  public Guid CourseId { get; set; }
  [JsonIgnore]
  public Course Course { get; set; }
}