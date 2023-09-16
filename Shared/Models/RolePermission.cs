using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class RolePermission {
  public int Id { get; set; }
  public Guid RoleId { get; set; }
  public Guid PermissionId { get; set; }
  [JsonIgnore]
  public Role? Role { get; set; }
  [JsonIgnore]
  public Permission? Permission { get; set; }
}