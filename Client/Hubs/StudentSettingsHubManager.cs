using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace edu_institutional_management.Client.Hubs;

public class StudentSettingsHubManager : HubManagerBase {
  public StudentSettingsHubManager(NavigationManager navigationManager) {
    _hubConnection = new HubConnectionBuilder().WithUrl(navigationManager.ToAbsoluteUri("/studentSettingsHub")).Build();
  }

  public void StudentSettingsUpdatedHandler(Action<StudentSettings> handler) {
    _hubConnection.On("StudentSettingsUpdated", handler);
  }

  public async Task SendStudentSettingsUpdatedAsync(string groupName) {
    await _hubConnection.SendAsync("SendStudentSettingsUpdate", groupName);
  }
}