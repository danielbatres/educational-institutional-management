using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace edu_institutional_management.Client.Hubs;

public class UsersHubManager : HubManagerBase {
  public UsersHubManager(NavigationManager navigationManager) {
    _hubConnection = new HubConnectionBuilder().WithUrl(navigationManager.ToAbsoluteUri("/usersHub")).Build();
  }

  public void UsersUpdatedHandler(Action<List<User>> handler) {
    _hubConnection.On("UsersUpdated", handler);
  }

  public async Task SendUsersUpdatedAsync(string groupName) {
    await _hubConnection.SendAsync("SendUsersUpdate", groupName);
  }
}