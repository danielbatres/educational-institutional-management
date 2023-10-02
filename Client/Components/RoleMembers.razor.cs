using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Components; 

public partial class RoleMembers {
  [Inject]
  private UserRoleHubManager UserRoleHubManager { get; set; }
  [Inject]
  private UsersHubManager UsersHubManager { get; set; }
  [Inject]
  private RolesHubManager RoleHubManager { get; set; }
  [Inject]
  private UserContext UserContext { get; set; }
  [Inject]
  private RoleContext RoleContext { get; set; }
  [Inject]
  private SettingsContext SettingsContext { get; set; }
  [Inject]
  private StatusModalContext StatusModalContext { get; set; }
  [Inject]
  private IUserRoleService UserRoleService { get; set; }
  [Inject]
  private IActivityService ActivityService { get; set; }
  private Role Role { get; set; } = new();
  private List<UserRole> UserRoles { get; set; } = new();
  private List<User> Users { get; set; } = new();

  protected override async Task OnInitializedAsync() {
    UsersHubManager.UsersUpdatedHandler(users => {
      Users = users;
      StateHasChanged();
    });

    UserRoleHubManager.UserRolesUpdatedHandler(userRole => {
      UserRoles = userRole;
      StateHasChanged();
    });

    RoleHubManager.RolesUpdatedHandler(roles => {
      Role = roles.FirstOrDefault(r => r.Id.Equals(RoleContext.ActualRoleIdSelection)) ?? new();
      StateHasChanged();
    });

    string groupName = UserContext.User.InstitutionId.ToString() ?? string.Empty;

    await RoleHubManager.SendRolesUpdatedAsync(groupName);
    await UsersHubManager.SendUsersUpdatedAsync(groupName);
    await UserRoleHubManager.SendUserRolesUpdatedAsync(groupName);
  }

  private void AddMember() { 
    SettingsContext.SetShowSettingsOption(Models.ShowSettingsOption.AddRoleMember);
    SettingsContext.SetShowSettingsModal(true);
  }

  private async Task RemoveMember(User user) {
    await UserRoleService.Delete(user.Id);

    ActivityLog activity = new() {
      ActionType = ActionType.Delete,
      Author = $"{UserContext.User.Name} {UserContext.User.LastName}",
      UserName = user.UserName,
      Date = DateTime.Now,
      Title = "Ha eliminado un miembro en un rol",
      Message = $"Ha eliminado el miembro {user.Name} {user.LastName} del rol {Role.Name}"
    };

    await ActivityService.Create(activity);
    await StatusModalContext.SetStatus(StatusType.Success);
  }
}