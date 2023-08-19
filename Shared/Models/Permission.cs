namespace edu_institutional_management.Shared.Models;

public class Permission {
  public Guid Id { get; set; }
  public string? Name { get; set; }
  public string? Description { get; set; }
  public Role? Role { get; set; }
}