using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Components;

public partial class Students {
  [Inject]
  private NavigationManager _navigationManager { get; set; }
  [Inject]
  private UserContext _userContext { get; set; }
  [Inject]
  private StudentHubManager _studentHubManager { get; set; }
  public string Route { get; set; }
  public string BaseRoute { get; set; }
  private int currentStudentIndex = -1;
  private bool Loading { get; set; } = true;
  private List<Student> StudentsList { get; set; } = new();

  protected override async Task OnInitializedAsync() {
    await Task.Delay(500);
    
    _studentHubManager.StudentsUpdatedHandler(students => {
      StudentsList = students;
      StateHasChanged();
    });

    await _studentHubManager.SendStudentsUpdatedAsync(_userContext.User.InstitutionId.ToString() ?? string.Empty);
    
    Loading = false;

    await ShowStudentsOneByOne();
  }

  private async Task ShowStudentsOneByOne() {
    while (currentStudentIndex < 9) {
      await Task.Delay(65);
      currentStudentIndex++;
      StateHasChanged();
    }
  }

  protected override void OnInitialized() {
    BaseRoute = $"/application/{_userContext.User.InstitutionId}/students/";
  }
}