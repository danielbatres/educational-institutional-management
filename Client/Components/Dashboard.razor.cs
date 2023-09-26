using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Components;

public partial class Dashboard {
  private bool IsSelected = false;
  [Inject]
  private ContentContext ContentContext { get; set; }
  [Inject]
  private UsersHubManager _usersHubManager { get; set; }
  [Inject]
  private UserContext UserContext { get; set; }
  [Inject]
  private NavigationManager _navigationManager { get; set; }
  [Inject]
  private IUserService _userService { get; set; }
  [Inject]
  private SideBarContext SideBarContext { get; set; }
  private List<User> Users = new();
  private User SelectedUser { get; set; } = new();
  private string SelectedUserTop { get; set; } = string.Empty;

  protected override async Task OnInitializedAsync() {
    SideBarContext.SetSelectedOptionMainMenu(0);
    ContentContext.SetSectionContent("Dashboard", "Mi panel");

    _usersHubManager.UsersUpdatedHandler(users => {
      Users = users;
      StateHasChanged();
    });

    await _usersHubManager.SendUsersUpdatedAsync(UserContext.User.InstitutionId.ToString() ?? string.Empty);
  }

  private async Task Disconnect() {
    UserContext.User.OnlineStatus.Status = false;
    UserContext.User.OnlineStatus.LastConnection = DateTime.Now;
    await _userService.Update(UserContext.User);

    await _usersHubManager.SendUsersUpdatedAsync(UserContext.User.InstitutionId.ToString() ?? string.Empty);
  }

  private void SetSelectedUser(int index) {
    IsSelected = !IsSelected;

    SelectedUser = Users[index];
    if (index == 0) {
      SelectedUserTop = "1%";
    } else {
      SelectedUserTop = (index + 1) * 6 + "%";
    }
  }
}