using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace edu_institutional_management.Client.Hubs;

public class CourseHubManager : HubManagerBase {
  public CourseHubManager(NavigationManager navigationManager) {
    _hubConnection = new HubConnectionBuilder().WithUrl(navigationManager.ToAbsoluteUri("/courseHub")).Build();
  }

  public void CoursesUpdatedHandler(Action<List<Course>> handler) {
    _hubConnection.On("CoursesUpdated", handler);
  }

  public async Task SendCoursesUpdatedAsync(string groupName) {
    await _hubConnection.SendAsync("SendCoursesUpdate", groupName);
  }
}