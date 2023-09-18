using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace edu_institutional_management.Client.Hubs;

public class SubjectHubManager : HubManagerBase {
  public SubjectHubManager(NavigationManager navigationManager) {
    _hubConnection = new HubConnectionBuilder().WithUrl(navigationManager.ToAbsoluteUri("/subjectHub")).Build();
  }

  public void SubjectsUpdatedHandler(Action<List<Subject>> handler) {
    _hubConnection.On("SubjectsUpdated", handler);
  }

  public async Task SendSubjectsUpdatedAsync(string groupName) {
    await _hubConnection.SendAsync("SendSubjectsUpdate", groupName);
  }
}