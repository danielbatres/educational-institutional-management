using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class Permission {
  public Guid Id { get; set; }
  public PermissionName Name { get; set; }
  public string? Description { get; set; }
  [JsonIgnore]
  public ICollection<RolePermission>? RolePermissions { get; set; }
}

public enum PermissionName {
  Administrator,
  SeeActivity,
  SeeRoles,
  HandleRolesSettings,
  SetMembersRoles,
  HandleStudentsSettings,
  HandleUsersSettings,
  HandleCoursesSettings,
  HandleInstitutionSettings,
  HandleStatistics,
  HandleEvents,
  SeeEvents,
  SeeStudents,
  HandleStudents,
  SeeCourses,
  HandleCourses
}