namespace edu_institutional_management.Shared.Models;

public class Schedule {
  public Guid Id { get; set; }
  public DayOfWeek DayOfWeek { get; set; }
  public TimeSpan StartTime { get; set; }
  public TimeSpan EndTime { get; set; }
}