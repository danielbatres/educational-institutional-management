namespace edu_institutional_management.Shared.Models;

public class Course {
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Acronym { get; set; }
  public string Guide { get; set; }
  public int StudentsCount { get; set; }
  public ICollection<Student>? Students { get; set; }
  public ICollection<SubjectCourse>? SubjectCourses { get; set; }
  public ICollection<AttendanceSchedule>? AttendanceSchedules { get; set; }
}