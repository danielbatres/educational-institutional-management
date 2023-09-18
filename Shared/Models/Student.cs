namespace edu_institutional_management.Shared.Models;

public class Student : MainUser {
  public string? UniqueIdentifier { get; set; }
  public string? Gender { get; set; }
  public Guid? CourseId { get; set; }
  public Course? Course { get; set; }
  public StudentRegister StudentRegister { get; set; }
  public ICollection<Attendance>? Attendances { get; set; }
  public ICollection<FieldInformation>? FieldsInformation { get; set; }
}