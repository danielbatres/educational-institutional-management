using edu_institutional_management.Shared.Models;
using edu_institutional_management.Server.Services;
using Microsoft.AspNetCore.SignalR;

namespace edu_institutional_management.Server.Hubs;

public class StudentSettingsHub : MainHub {
  private readonly IStudentSettingsService _studentSettingsService;
  
  public StudentSettingsHub(IStudentSettingsService studentSettingsService) {
    _studentSettingsService = studentSettingsService;
  }
  
  public async Task SendStudentSettingsUpdate(string groupName) {
    var studentSettings = GetStudentSettings();
    await Clients.Group(groupName).SendAsync("StudentSettingsUpdated", studentSettings);
  }
  
  public StudentSettings GetStudentSettings() {
    return _studentSettingsService.Get().FirstOrDefault() ?? new();
  }
}