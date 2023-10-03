using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Client.Models;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Components;

public partial class CourseInfo {

  [Inject]
  private ICourseService CourseService { get; set; }
  [Inject]
  private CourseHubManager CourseHubManager { get; set; }
  [Inject]
  private CourseContext CourseContext { get; set; }
  [Inject]
  private UserContext UserContext { get; set; }
  [Inject]
  private SectionContext SectionContext { get; set; }
  private string Selection { get; set; } = "general";
  [Inject]
  private StatusModalContext StatusModalContext { get; set; }
  private int NavigationOption { get; set; }
  private List<Course> Courses { get; set; }
  private List<List<object>> Warnings { get; set; } = new() {
    new() { "", false },
    new() { "", false }
  };

  protected override void OnInitialized() {
    StatusModalContext.SetAcceptWarning(false);
    StatusModalContext.SetShowWarning(false);
  }

  protected override async Task OnInitializedAsync() {
     CourseHubManager.CoursesUpdatedHandler(courses => {
      Courses = courses;
      StateHasChanged();
    });
  
    string groupName = UserContext.User.InstitutionId.ToString() ?? string.Empty;

    await CourseHubManager.SendCoursesUpdatedAsync(groupName);
  }

    private void AddSubjectCourse() {
        SectionContext.SetShowSectionOption(ShowSectionOptions.AddSubjectCourse);
        SectionContext.SetShowSectionModal(true);
    }

    private void AddCourseGuide() {
        SectionContext.SetShowSectionOption(ShowSectionOptions.AddCourseGuide);
        SectionContext.SetShowSectionModal(true);
    }

  private void SetSelection(string selection) {
    Selection = selection;

    switch (selection) {
      case "general":
        NavigationOption = 0;
        break;
      case "students-course":
        NavigationOption = 1;
        break;
      case "calendar":
        NavigationOption = 2;
        break;
    }
  } 
  
    private async Task CreateNewCourse() {
        await CourseService.Create();

        CourseContext.SetNewCourse();
        await CourseHubManager.SendCoursesUpdatedAsync(UserContext.User.InstitutionId.ToString() ?? string.Empty);
    }

  private async Task SaveChanges() {
    await StatusModalContext.SetStatus(StatusType.Warning);
    StatusModalContext.SetWarningMessage("Campos vacios", "¿Estas seguro que quieres continuar? aun tienes campos vacios que ingresar");
    StatusModalContext.SetShowWarning(true);
  }
}