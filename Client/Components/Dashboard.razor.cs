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
  [Inject]
  private StudentHubManager StudentHubManager { get; set; }
  [Inject]
  private ActivityHubManager ActivityHubManager { get; set; }
  private List<ActivityLog> Activities { get; set; }
  private int MaxActivityCount { get; set; }
  private User SelectedUser { get; set; } = new();
  private string SelectedUserTop { get; set; } = string.Empty;
  private int StudentsCount { get; set; } = 0;
  private int UsersCount { get; set; } = 0;

  protected override async Task OnInitializedAsync() {
    SideBarContext.SetSelectedOptionMainMenu(0);
    ContentContext.SetSectionContent("Dashboard", "Mi panel");

    StudentHubManager.StudentsUpdatedHandler(async (students) => {
      StudentsCount = 0;

      for (int i = 0; i < students.Count; i++) {
        StudentsCount++;
        StateHasChanged();
        await Task.Delay(1);
      }
    });

    _usersHubManager.UsersUpdatedHandler(async (users) => {
      UsersCount = 0;

      for (int i = 0; i < users.Count; i++) {
        UsersCount++;
        StateHasChanged();
        await Task.Delay(1);
      }
    });

    ActivityHubManager.ActivityUpdatedHandler(activities => {
      Activities = activities;
      StateHasChanged();
      
    });

    string groupName = UserContext.User.InstitutionId.ToString() ?? string.Empty;

    await _usersHubManager.SendUsersUpdatedAsync(groupName);
    await StudentHubManager.SendStudentsUpdatedAsync(groupName);
    await ActivityHubManager.SendActivityUpdatedAsync(groupName);
  }

  private async Task Disconnect() {
    UserContext.User.OnlineStatus.Status = false;
    UserContext.User.OnlineStatus.LastConnection = DateTime.Now;
    await _userService.Update(UserContext.User);

    await _usersHubManager.SendUsersUpdatedAsync(UserContext.User.InstitutionId.ToString() ?? string.Empty);
  }
}