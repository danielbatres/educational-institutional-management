namespace edu_institutional_management.Shared.DTO;

public class StudentDto {
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string LastName { get; set; }
  public string Photo { get; set; }
  public string Email { get; set; }
  public string FullName { get; set; }
  public string Gender { get; set; }
  public byte Age { get; set; }
  public DateTime CreatedAt { get; set; }
}