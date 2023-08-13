namespace edu_institutional_management.Shared.DTO;

public class CourseDto {
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Guide { get; set; }
  public uint StudentsQuantity { get; set; }
  public List<StudentDto> Students { get; set; }
}