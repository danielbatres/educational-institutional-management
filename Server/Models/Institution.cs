namespace edu_institutional_management.Server.Models;

public class Institution {
  public Guid Id { get; set; }
  public string? Name { get; set; }
  public string? Address { get; set; }
  public string? PhoneNumber { get; set; }
  public ICollection<User> Users { get; set; }
}