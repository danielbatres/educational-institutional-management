namespace edu_institutional_management.Shared.DTO;

public class InstitutionDto {
  public Guid Id { get; set; }
  public string? Name { get; set; }
  public string? Address { get; set; }
  public string? PhoneNumber { get; set; }
  public ICollection<UserDto> Users { get; set; }
}