using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace edu_institutional_management.Client.Hubs;

public class StudentHubManager : HubManagerBase {
  public StudentHubManager(NavigationManager navigationManager) {
    _hubConnection = new HubConnectionBuilder().WithUrl(navigationManager.ToAbsoluteUri("/studentsHub")).Build();
  }
  
  public void StudentsUpdatedHandler(Action<List<Student>> handler) {
    _hubConnection.On("StudentsUpdated", handler);
  }

  public async Task SendStudentsUpdatedAsync(string groupName) {
    await _hubConnection.SendAsync("SendStudentsUpdate", groupName);
  }
}