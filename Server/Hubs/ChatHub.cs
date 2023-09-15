using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace edu_institutional_management.Server.Hubs;

public class ChatHub : MainHub {
  private readonly IChatService _chatService;
  
  public ChatHub(IChatService chatService) {
    _chatService = chatService;
  }
  
  public async Task SendChatUpdate(string groupName) {
    var chatMessages = GetChatMessages();
    
    await Clients.Group(groupName).SendAsync("ChatUpdated", chatMessages);
  }
  
  public List<ChatMessage> GetChatMessages() {
    return _chatService.Get().ToList();
  }
}