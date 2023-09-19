using edu_institutional_management.Client.Services;
using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace edu_institutional_management.Client.Components;

public partial class CourseSettings {
  [Inject]
  private ISubjectService _subjectService { get; set; }
  [Inject]
  private ICourseService _courseService { get; set; }
  [Inject]
  private CourseContext _courseContext { get; set; }
  [Inject]
  private CourseHubManager _courseHubManager { get; set; }
  [Inject]
  private SubjectHubManager _subjectHubManager { get; set; }
  [Inject]
  private UserContext _userContext { get; set; }
  [Inject]
  private ISubjectCourseService _subjectCourseService { get; set; }
  [Inject]
  private ICourseScheduleService _courseScheduleService { get; set; }
  private Subject Subject { get; set; } = new();
  private List<Subject> Subjects { get; set; } = new();
  private SubjectCourse SubjectCourse { get; set; }
  private List<SubjectCourse> SubjectsCourse { get; set; } = new();
  private CourseSchedule CourseSchedule { get; set; } = new();
  private List<Course> Courses { get; set; } = new();
  private int RenderCount { get; set; } = 0;
  private string SubjectId { get; set; } = string.Empty;

  protected override void OnInitialized() {
    AssignNewSubject();
    AssignNewCourseSchedule();
    _courseContext.SetNewCourse();
  }
  
  protected override async Task OnInitializedAsync() {
    _subjectHubManager.SubjectsUpdatedHandler(subjects => {
      Subjects = subjects;
      StateHasChanged();
    });
  
    _courseHubManager.CoursesUpdatedHandler(courses => {
      Courses = courses;
      StateHasChanged();
    });
  
    string groupName = _userContext.User.InstitutionId.ToString() ?? string.Empty;
  
    await _subjectHubManager.SendSubjectsUpdatedAsync(groupName);
    await _courseHubManager.SendCoursesUpdatedAsync(groupName);
  }
  
  protected override async Task OnAfterRenderAsync(bool isFirstRender) {
    /*if (RenderCount < 2 && RenderCount > 0) {
      if (Courses.Count > 0)
        SubjectsCourse = await _subjectCourseService.Get(Courses[0].Id);
    }

    RenderCount += 1;*/
  }
  
  private void AssignNewSubject() {
    Subject = new() {
      Id = Guid.NewGuid(),
      Name = string.Empty
    };
  }
  
  private void AssignNewCourseSchedule() {
    CourseSchedule = new() {
      Id = Guid.NewGuid()
    };
  }
  
  private async Task CreateNewSubject() {
    await _subjectService.Create(Subject);
  
    AssignNewSubject();
  }
  
  private async Task CreateNewCourse() {
    await _courseService.Create();
  
    _courseContext.SetNewCourse();
    await _courseHubManager.SendCoursesUpdatedAsync(_userContext.User.InstitutionId.ToString() ?? string.Empty);
  }
  
  private async Task CreateNewCourseSubject(Guid subjectId, Guid courseId) {
    Guid subjectCourseId = Guid.NewGuid();
      
    await _subjectCourseService.Create(new () {
      Id  = subjectCourseId,
      CourseId = courseId,
      SubjectId = subjectId
    });
    
    CourseSchedule.SubjectCourseId = subjectCourseId;
    
    await _courseScheduleService.Create(CourseSchedule);
    AssignNewCourseSchedule();
    SubjectId = string.Empty;
  }
}