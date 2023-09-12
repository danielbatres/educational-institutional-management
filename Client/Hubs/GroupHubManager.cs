using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace edu_institutional_management.Client.Hubs;

public class GroupHubManager : HubManagerBase {
  public GroupHubManager(NavigationManager navigationManager)
  {
    _hubConnection = new HubConnectionBuilder().WithUrl(navigationManager.ToAbsoluteUri("/groupHub")).Build();
  }

  public async Task JoinGroup(string groupName) {
    await _hubConnection.SendAsync("JoinGroup", groupName);
  }

  public async Task LeaveGroup(string groupName) {
    await _hubConnection.SendAsync("LeaveGroup", groupName);
  }
}