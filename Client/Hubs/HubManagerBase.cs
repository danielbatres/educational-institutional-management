using Microsoft.AspNetCore.SignalR.Client;

namespace edu_institutional_management.Client.Hubs;

public class HubManagerBase {
  protected HubConnection _hubConnection;

  public async Task StartConnectionAsync() {
    await _hubConnection.StartAsync();
  }

  public async Task DisposeConnectionAsync() {
    if (_hubConnection != null) {
      await _hubConnection.DisposeAsync();
    }
  }

  public async Task JoinGroup(string groupName) {
    await _hubConnection.SendAsync("JoinGroup", groupName);
  }

  public async Task LeaveGroup(string groupName) {
    await _hubConnection.SendAsync("LeaveGroup", groupName);
  }
}