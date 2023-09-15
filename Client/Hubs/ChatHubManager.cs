using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace edu_institutional_management.Client.Hubs;

public class ChatHubManager : HubManagerBase {
  public ChatHubManager(NavigationManager navigationManager) {
    _hubConnection = new HubConnectionBuilder().WithUrl(navigationManager.ToAbsoluteUri("/chatHub")).Build();
  }
  
  public void ChatUpdatedHandler(Action<List<ChatMessage>> handler) {
    _hubConnection.On("ChatUpdated", handler);
  }

  public async Task SendChatUpdatedAsync(string groupName) {
    await _hubConnection.SendAsync("SendChatUpdate", groupName);
  }
}