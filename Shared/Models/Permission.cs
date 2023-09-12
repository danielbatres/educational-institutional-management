namespace edu_institutional_management.Shared.Models;

public class Permission {
  public Guid Id { get; set; }
  public PermissionName Name { get; set; }
  public string? Description { get; set; }
  public ICollection<RolePermission>? RolePermissions { get; set; }
}

public enum PermissionName {
  SeeActivity,
}