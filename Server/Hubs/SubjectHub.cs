using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace edu_institutional_management.Server.Hubs;

public class SubjectHub : MainHub {
  private readonly ISubjectService _subjectService;

  public SubjectHub(ISubjectService subjectService) {
    _subjectService = subjectService;
  }

  public async Task SendSubjectsUpdate(string groupName) {
    var subjects = GetSubjects();
  
    await Clients.Group(groupName).SendAsync("SubjectsUpdated", subjects);
  }

  public List<Subject> GetSubjects() {
    return _subjectService.Get().ToList();
  }
}