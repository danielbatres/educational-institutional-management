using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Components;

public partial class OnlineUsers {
  private bool IsSelected = false;
  [Inject]
  private UsersHubManager UsersHubManager { get; set; }
  [Inject]
  private UserContext UserContext { get; set; }
  [Inject]
  private IUserService UserService { get; set; }
  [Inject]
  private RolesHubManager RolesHubManager { get; set; }
  [Inject]
  private UserRoleHubManager UserRoleHubManager { get; set; }
  private List<User> Users { get; set; } = new();
  private List<Role> Roles { get; set; } = new();
  private List<UserRole> UserRoles { get; set; } = new();
  private User SelectedUser { get; set; } = new();
  private string SelectedUserTop { get; set; } = string.Empty;
  private bool Loading { get; set; } = true;

  protected override async Task OnInitializedAsync() {
    UsersHubManager.UsersUpdatedHandler(users => {
      Users = users;
      StateHasChanged();
    });

    RolesHubManager.RolesUpdatedHandler(roles => {
      Roles = roles;
      StateHasChanged();
    });

    UserRoleHubManager.UserRolesUpdatedHandler(userRole => {
      UserRoles = userRole;
      StateHasChanged();
    });

    string groupName = UserContext.User.InstitutionId.ToString() ?? string.Empty;

    await RolesHubManager.SendRolesUpdatedAsync(groupName);
    await UsersHubManager.SendUsersUpdatedAsync(groupName);
    await UserRoleHubManager.SendUserRolesUpdatedAsync(groupName);

    await Task.Delay(500);
    Loading = false;
  }

  private async Task Disconnect() {
    UserContext.User.OnlineStatus.Status = false;
    UserContext.User.OnlineStatus.LastConnection = DateTime.Now;
    await UserService.Update(UserContext.User);

    await UsersHubManager.SendUsersUpdatedAsync(UserContext.User.InstitutionId.ToString() ?? string.Empty);
  }

  private void SetSelectedUser(int index) {
    IsSelected = !IsSelected;

    SelectedUser = Users[index];
    if (index == 0) {
      SelectedUserTop = "1%";
    }
    else
    {
      SelectedUserTop = (index + 1) * 6 + "%";
    }
  }
}