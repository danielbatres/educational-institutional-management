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
}