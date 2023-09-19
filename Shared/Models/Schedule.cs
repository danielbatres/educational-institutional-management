namespace edu_institutional_management.Shared.Models;

public class Schedule {
  public Guid Id { get; set; }
  public DayOfWeek DayOfWeek { get; set; }
  public DateTime StartTime { get; set; }
  public DateTime EndTime { get; set; }
}