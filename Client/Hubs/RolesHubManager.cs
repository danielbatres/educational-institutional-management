using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace edu_institutional_management.Client.Hubs;

public class RolesHubManager : HubManagerBase {
  public RolesHubManager(NavigationManager navigationManager) {
    _hubConnection = new HubConnectionBuilder().WithUrl(navigationManager.ToAbsoluteUri("/rolesHub")).Build();
  }

  public void RolesUpdatedHandler(Action<List<Role>> handler) {
    _hubConnection.On("RolesUpdated", handler);
  }

  public async Task SendGroupName(string groupName) {
    await StartConnectionAsync();
    await _hubConnection.SendAsync("JoinInstitutionGroup", groupName);
  }

  public async Task SendRolesUpdatedAsync(string groupName) {
    await _hubConnection.SendAsync("SendRolesUpdate", groupName);
  }
}