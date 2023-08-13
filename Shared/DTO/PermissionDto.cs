namespace edu_institutional_management.Shared.DTO;

public class PermissionDto {
  public Guid Id { get; set; }
  public string? Name { get; set; }
  public string? Description { get; set; }
  public RoleDto? Role { get; set; }
}