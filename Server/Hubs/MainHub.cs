using Microsoft.AspNetCore.SignalR;

namespace edu_institutional_management.Server.Hubs;

public class MainHub : Hub {
  public async Task JoinGroup(string groupName) {
    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
  }

  public async Task LeaveGroup(string groupName) {
    await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
  }
}