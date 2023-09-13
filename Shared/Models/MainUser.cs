namespace edu_institutional_management.Shared.Models;

public class MainUser {
  public Guid Id { get; set; }
  public string? Name { get; set; }
  public string? LastName { get; set; }
  public string? Photo { get; set; }
  public DateTime? BirthDate { get; set; }
  public int Age { get; set; }
  public string? PhoneNumber { get; set; }
}