namespace edu_institutional_management.Shared.DTO;

public class UserDto {
  public Guid Id { get; set; }
  public string? Name { get; set; }
  public string? LastName { get; set; }
  public DateTime? BirthDate { get; set; }
  public int Age { get; set; }
  public string? PhoneNumber { get; set; }
  public string? Bio { get; set; }
  public InstitutionDto Institution { get; set; }
  public Guid InstitutionId { get; set; }
  public OnlineStatusDto OnlineStatus { get; set; }
  public RegisterDto Register { get; set; }
}