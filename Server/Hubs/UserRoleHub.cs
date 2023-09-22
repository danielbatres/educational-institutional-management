using edu_institutional_management.Client.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace edu_institutional_management.Server.Hubs;

public class UserRoleHub : MainHub {
  private readonly IUserRoleService _userRoleService;

  public UserRoleHub(IUserRoleService userRoleService) {
    _userRoleService = userRoleService;
  }

  public async Task SendUsersRoleUpdate(string groupName) {
    var userRoles = GetUserRoles();

    await Clients.Group(groupName).SendAsync("UserRoleSUpdated", userRoles);
  }

  public List<UserRole> GetUserRoles() {
    return _userRoleService.Get().ToList();
  }
}