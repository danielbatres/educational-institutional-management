namespace edu_institutional_management.Shared.Models;

public class Role {
  public Guid Id { get; set; }
  public string? Name { get; set; }
  public List<Permission>? Permissions { get; set; }
}