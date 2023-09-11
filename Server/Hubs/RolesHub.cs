using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace edu_institutional_management.Server.Hubs;

public class RolesHub : MainHub {
  private readonly IRoleService _roleService;

  public RolesHub(IRoleService roleService){
    _roleService = roleService;
  }

  public async Task SendRolesUpdate(string groupName) {
    var roles = GetRoles();
    await Clients.Group(groupName).SendAsync("RolesUpdated", roles);
  }

  public string GroupName { get; set; }

  public async Task JoinInstitutionGroup(string groupName) {
    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
  }

  public List<Role> GetRoles() {
    return _roleService.Get().ToList();
  }
}