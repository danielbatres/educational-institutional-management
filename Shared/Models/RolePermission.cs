namespace edu_institutional_management.Shared.Models;

public class RolePermission {
  public Guid RoleId { get; set; }
  public Guid PermissionId { get; set; }
  public Role? Role { get; set; }
  public Permission? Permission { get; set; }
}