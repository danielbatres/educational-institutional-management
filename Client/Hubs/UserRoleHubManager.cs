using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace edu_institutional_management.Client.Hubs;

public class UserRoleHubManager : HubManagerBase {
  public UserRoleHubManager(NavigationManager navigationManager) {
    _hubConnection = new HubConnectionBuilder().WithUrl(navigationManager.ToAbsoluteUri("/userRoleHub")).Build();
  }

  public void UserRolesUpdatedHandler(Action<List<UserRole>> handler) {
    _hubConnection.On("UserRolesUpdated", handler);
  }

  public async Task SendUserRolesUpdatedAsync(string groupName)
  {
    await _hubConnection.SendAsync("SendUsersRoleUpdate", groupName);
  }
}