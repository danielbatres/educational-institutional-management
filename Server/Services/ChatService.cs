using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class ChatService : BaseService, IChatService {
  public ChatService(ApplicationContextService applicationContextService) : base (applicationContextService) {}

  public IEnumerable<ChatMessage> Get() {
    return _applicationContext.ChatMessages;
  }
  
  public async Task Create(ChatMessage chat) {
    _applicationContext.ChatMessages.Add(chat);
  
    await _applicationContext.SaveChangesAsync();
  }
}

public interface IChatService {
  Task Create(ChatMessage chat);
  IEnumerable<ChatMessage> Get();
}