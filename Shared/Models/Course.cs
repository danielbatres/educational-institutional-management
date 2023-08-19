namespace edu_institutional_management.Shared.Models;
public class CourseDto {
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Guide { get; set; }
  public uint StudentsQuantity { get; set; }
  public List<Student> Students { get; set; }
}