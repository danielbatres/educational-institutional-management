using System.Text.RegularExpressions;
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
  [Inject]
  private Validators Validators { get; set; }
  [Inject]
  private NavigationManager NavigationManager { get; set; }
  [Inject]
  private SubjectHubManager SubjectHubManager { get; set; }
  [Inject]
  private ISubjectCourseService SubjectCourseService { get; set; }
  private List<SubjectCourse> SubjectCourses { get; set; } = new();
  private List<Subject> Subjects { get; set; } = new();
  private int NavigationOption { get; set; }
  private List<Course> Courses { get; set; } = new();
  private List<List<object>> Warnings { get; set; } = new() {
    new() { "", false },
    new() { "", false }
  };
  private int ErrorsCount { get; set; }

  private async Task RemoveSubject(Guid subjectId) {
    Guid subjectCourseId = SubjectCourses.FirstOrDefault(s => s.SubjectId.Equals(subjectId))?.Id ?? Guid.Empty;

    if (subjectCourseId != Guid.Empty) {
      await SubjectCourseService.Delete(subjectCourseId);

      string groupName = UserContext.User.InstitutionId.ToString() ?? string.Empty;

      await SubjectHubManager.SendSubjectsUpdatedAsync(groupName);
      await CourseHubManager.SendCoursesUpdatedAsync(groupName);
      await StatusModalContext.SetStatus(StatusType.Success); 
    }
  }

  protected override void OnInitialized() {
    StatusModalContext.SetAcceptWarning(false);
    StatusModalContext.SetShowWarning(false);
  }

  protected override async Task OnInitializedAsync() {
     CourseHubManager.CoursesUpdatedHandler(courses => {
      Courses = courses;
      StateHasChanged();
    });

    SubjectHubManager.SubjectsUpdatedHandler(async subjects => {
      Subjects = subjects;
      SubjectCourses = await SubjectCourseService.Get(CourseContext.Course.Id);
      StateHasChanged();
    });
  
    string groupName = UserContext.User.InstitutionId.ToString() ?? string.Empty;
    await CourseHubManager.SendCoursesUpdatedAsync(groupName);
    await SubjectHubManager.SendSubjectsUpdatedAsync(groupName);
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

  private List<object> ValidateRequired(string text) {
    string warning = Validators.IsRequired(text);
    bool option = false;

    if (warning != string.Empty) {
      option = true;
      ErrorsCount++;
    }

    return new() { warning, option };
  }

  private async Task CourseAction() {
    ErrorsCount = 0;

    Warnings[0] = ValidateRequired(CourseContext.Course.Acronym);
    Warnings[1] = ValidateRequired(CourseContext.Course.Name);

    if (ErrorsCount != 0) {
      await StatusModalContext.SetStatus(StatusType.Danger);
      
      return;
    }

    switch (CourseContext.CourseOption) {
      case ActionType.Create:
        await CourseService.Create();
        CourseContext.SetNewCourse();
        break;
      case ActionType.Update:
        await CourseService.Update();
        break;
    }

    NavigationManager.NavigateTo($"/application/{UserContext.User.InstitutionId}/courses");
  }

  private async Task SaveChanges() {
    await StatusModalContext.SetStatus(StatusType.Warning);
    StatusModalContext.SetWarningMessage("Campos vacios", "¿Estas seguro que quieres continuar? aun tienes campos vacios que ingresar");
    StatusModalContext.SetShowWarning(true);
  }
}