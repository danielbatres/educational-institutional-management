using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace edu_institutional_management.Client.Hubs;

public class GeneralNotificationHubManager : HubManagerBase {
  public GeneralNotificationHubManager(NavigationManager navigationManager) {
    _hubConnection = new HubConnectionBuilder().WithUrl(navigationManager.ToAbsoluteUri("/generalNotificationHub")).Build();
  }

  public void GeneralNotificationUpdatedHandler(Action<List<GeneralNotification>> handler) {
    _hubConnection.On("GeneralNotificationUpdated", handler);
  }

  public async Task SendGeneralNotificationUpdatedAsync(string groupName) {
    await _hubConnection.SendAsync("SendGeneralNotificationUpdate", groupName);
  }
}