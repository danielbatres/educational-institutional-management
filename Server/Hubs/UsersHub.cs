using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace edu_institutional_management.Server.Hubs;

public class UsersHub : MainHub {
  private readonly IUserService _userService;
  
  public UsersHub(IUserService userService) {
    _userService = userService;
  }
  
  public async Task SendUsersUpdate(string groupName) {
    var users = GetInstitutionUsers(Guid.Parse(groupName));
    await Clients.Group(groupName).SendAsync("UsersUpdated", users);
  }

  public List<User> GetInstitutionUsers(Guid id) {
    return _userService.Get().Where(u => u.InstitutionId == id).ToList();
  }
}