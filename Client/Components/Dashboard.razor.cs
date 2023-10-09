using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Components;

public partial class Dashboard {
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
  [Inject]
  private NavigationManager NavigationManager { get; set; }
  private List<ActivityLog> Activities { get; set; }
  private List<Student> Students { get; set; } = new();
  private string FormattedDateTime { get; set; } = string.Empty;

  private async Task UpdateClock() {
    while (true) {
      var currentDateTime = DateTime.Now;

      FormattedDateTime = currentDateTime.ToString("dddd d, MMMM yyyy HH:mm");

      await Task.Delay(1000);
      StateHasChanged();
    }
  }

  protected override async Task OnInitializedAsync() {
    SideBarContext.SetSelectedOptionMainMenu(0);
    ContentContext.SetSectionContent("Dashboard", "Mi panel");

    ActivityHubManager.ActivityUpdatedHandler(activities => {
      Activities = activities;
      StateHasChanged();
    });

    StudentHubManager.StudentsUpdatedHandler(students => {
      Students = students;
      StateHasChanged();
    });

    string groupName = UserContext.User.InstitutionId.ToString() ?? string.Empty;

    await _usersHubManager.SendUsersUpdatedAsync(groupName);
    await StudentHubManager.SendStudentsUpdatedAsync(groupName);
    await ActivityHubManager.SendActivityUpdatedAsync(groupName);

    await UpdateClock();
  }

  private void GoStudents() {
    NavigationManager.NavigateTo($"/application/{UserContext.User.InstitutionId}/students");
  }

  private async Task Disconnect() {
    UserContext.User.OnlineStatus.Status = false;
    UserContext.User.OnlineStatus.LastConnection = DateTime.Now;
    await _userService.Update(UserContext.User);

    await _usersHubManager.SendUsersUpdatedAsync(UserContext.User.InstitutionId.ToString() ?? string.Empty);
  }
}