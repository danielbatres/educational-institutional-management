using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace edu_institutional_management.Client.Hubs;

public class ActivityHubManager : HubManagerBase {
  public ActivityHubManager(NavigationManager navigationManager) {
    _hubConnection = new HubConnectionBuilder().WithUrl(navigationManager.ToAbsoluteUri("/activityHub")).Build();
  }

  public void ActivityUpdatedHandler(Action<List<ActivityLog>> handler) {
    _hubConnection.On("ActivityUpdated", handler);
  }

  public async Task SendActivityUpdatedAsync(string groupName) {
    await _hubConnection.SendAsync("SendActivitiesUpdate", groupName);
  }
}