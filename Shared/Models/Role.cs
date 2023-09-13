using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class Role {
  public Guid Id { get; set; }
  public string? Name { get; set; }
  public string? RoleColor { get; set; }
  public string? Description { get; set; }
  public int MembersCount { get; set; }
  public int PermissionsCount { get; set; }
  [JsonIgnore]
  public List<RolePermission>? RolePermissions { get; set; }
}