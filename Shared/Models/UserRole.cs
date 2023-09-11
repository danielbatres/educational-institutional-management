namespace edu_institutional_management.Shared.Models;

public class UserRole {
  public Guid UserId { get; set; }
  public Guid RoleId { get; set; }
  public Role? Role { get; set; }
}