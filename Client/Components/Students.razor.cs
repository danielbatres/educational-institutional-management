using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace edu_institutional_management.Client.Components;

public partial class Students {
  [Inject]
  private NavigationManager _navigationManager { get; set; }
  [Inject]
  private UserContext _userContext { get; set; }
  [Inject]
  private StudentHubManager _studentHubManager { get; set; }
  [Inject]
  private StudentContext _studentContext { get; set; }
  [Inject]
  private StudentSettingsHubManager _studentSettingsHubManager { get; set; }
  private StudentSettings StudentSettings { get; set; } = new();
  public string Route { get; set; }
  public string BaseRoute { get; set; }
  private int currentStudentIndex = -1;
  private bool Loading { get; set; } = true;
  private List<Student> StudentsList { get; set; } = new();
  private List<Student> FilteredStudentsList { get; set; } = new();
  private string SearchValue { get; set; } = string.Empty;
  private bool IsBoxStyle { get; set; }
  private int RenderCount { get; set; } = 0;

  protected override async Task OnInitializedAsync() {
    _studentHubManager.StudentsUpdatedHandler(students => {
      StudentsList = students;
      StateHasChanged();
    });

    _studentSettingsHubManager.StudentSettingsUpdatedHandler(studentSettings => {
      StudentSettings = studentSettings;
      StateHasChanged();
    });

    string groupName = _userContext.User.InstitutionId.ToString() ?? String.Empty;

    await _studentHubManager.SendStudentsUpdatedAsync(groupName);
    await _studentSettingsHubManager.SendStudentSettingsUpdatedAsync(groupName);

    await Task.Delay(500);
    Loading = false;
    await ShowStudentsOneByOne();
  }

  protected override async Task OnAfterRenderAsync(bool firstRender) {
    if (RenderCount.Equals(2)) {
      FilteredStudentsList = StudentsList.ToList();
    }

    RenderCount++;
  }

  private async Task SearchStudents(KeyboardEventArgs e) {
    if (e.Key == "Enter") {
      if (!string.IsNullOrEmpty(SearchValue)) {
        FilteredStudentsList = StudentsList.Where(s =>
          s.Name.ToLower().Contains(SearchValue.ToLower()) ||
          s.LastName.ToLower().Contains(SearchValue.ToLower()) ||
          s.StudentRegister.Email.Contains(SearchValue.ToLower()) ||
          s.Id.ToString().ToLower().Contains(SearchValue.ToLower()) ||
          s.UniqueIdentifier.ToLower().Contains(SearchValue.ToLower())).ToList();
      } else {
        FilteredStudentsList = StudentsList.ToList();
      }
    }
  }

  private async Task ShowStudentsOneByOne() {
    while (currentStudentIndex < 9) {
      await Task.Delay(65);
      currentStudentIndex++;
      StateHasChanged();
    }
  }

  private void UpdateSearchValue(ChangeEventArgs e) {
    SearchValue = e.Value.ToString();
  }

  private void SetShowStudent(Student student) {
    _studentContext.SetStudent(student);
    _navigationManager.NavigateTo($"{BaseRoute}{student.Id}");
  }

  protected override void OnInitialized() {
    BaseRoute = $"/application/{_userContext.User.InstitutionId}/students/";
  }
}