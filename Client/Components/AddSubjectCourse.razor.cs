using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Components;

public partial class AddSubjectCourse {
  [Inject]
  private SubjectHubManager SubjectHubManager { get; set; }
  [Inject]
  private UserContext UserContext { get; set; }
  [Inject]
  private UsersHubManager UsersHubManager { get; set; }
  [Inject]
  private ISubjectCourseService SubjectCourseService { get; set; }
  [Inject]
  private CourseContext CourseContext { get; set; }
  [Inject]
  private CourseHubManager CourseHubManager { get; set; }
  [Inject]
  private ActivityHubManager ActivityHubManager { get; set; }
  [Inject]
  private IActivityService ActivityService { get; set; }
  [Inject]
  private StatusModalContext StatusModalContext { get; set; }
  [Inject]
  private SectionContext SectionContext { get; set; }
  private List<User> Users { get; set; }
  private User SelectedUser { get; set; } = new();
  private List<Subject> Subjects { get; set; } = new();
  private string SubjectId { get; set; } = string.Empty;
  private List<SubjectCourse> SubjectCourses { get; set; } = new();

  protected override async Task OnInitializedAsync() {
    SubjectHubManager.SubjectsUpdatedHandler(async subjects => {
      Subjects = subjects;
      SubjectCourses = await SubjectCourseService.Get(CourseContext.Course.Id);
      StateHasChanged();
    });

    UsersHubManager.UsersUpdatedHandler(users => {
      Users = users;
      StateHasChanged();
    });

    string groupName = UserContext.User.InstitutionId.ToString() ?? string.Empty;

    await SubjectHubManager.SendSubjectsUpdatedAsync(groupName);
    await UsersHubManager.SendUsersUpdatedAsync(groupName);
  }

  private async Task CreateNewCourseSubject() {
    if (SubjectId == string.Empty) {
      await StatusModalContext.SetStatus(StatusType.Warning);

      return;
    }

    await SubjectCourseService.Create(new() {
      Id = Guid.NewGuid(),
      CourseId = CourseContext.Course.Id,
      SubjectId = Guid.Parse(SubjectId),
      UserId = SelectedUser.Id
    });

    ActivityLog activity = new() {
      ActionType = ActionType.Create,
      Author = $"{UserContext.User.Name} {UserContext.User.LastName}",
      Title = "Ha agregado una asignatura a un curso",
      Message = $"Ha agregado la asignatura de {Subjects.FirstOrDefault(s => s.Id.Equals(Guid.Parse(SubjectId)))?.Name} al curso {CourseContext.Course.Name}",
      UserName = UserContext.User.UserName
    };

    string groupName = UserContext.User.InstitutionId.ToString() ?? string.Empty;
    SubjectId = string.Empty;

    await ActivityService.Create(activity);
    await ActivityHubManager.SendActivityUpdatedAsync(groupName);
    await CourseHubManager.SendCoursesUpdatedAsync(groupName);
    await SubjectHubManager.SendSubjectsUpdatedAsync(groupName); 
    SectionContext.SetShowSectionModal(false);
    await StatusModalContext.SetStatus(StatusType.Success);
  }
}