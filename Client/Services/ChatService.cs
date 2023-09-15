using edu_institutional_management.Shared.Models;
using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Hubs;
using System.Net.Http.Json;

namespace edu_institutional_management.Client.Services;

public class ChatService : BaseService, IChatService {
  private readonly UserContext _userContext;
  private readonly UsersHubManager _usersHubManager;
  
  public ChatService(HttpClient httpClient, UserContext userContext, UsersHubManager usersHubManager) : base(httpClient) {
    _userContext = userContext;
    _usersHubManager = usersHubManager;
  }
  
  public async Task Create(ChatMessage chatMessage) {
    var response = await HttpClient.PostAsJsonAsync("api/chat/create", chatMessage);
    
    await CheckResponse(response);
    
    await _usersHubManager.SendUsersUpdatedAsync(_userContext.User.InstitutionId.ToString());
  }
}

public interface IChatService {
  Task Create(ChatMessage chatMessage);
}