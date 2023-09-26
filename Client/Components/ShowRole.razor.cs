using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Client.Models;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Components;

public partial class ShowRole {
    private Role Role { get; set; } = new();
  [Inject]
  private RoleContext _roleContext { get; set; }
  [Inject]
  private RolesHubManager _rolesHubManager { get; set; }
  [Inject]
  private UserContext _userContext { get; set; }
  private string MenuSelection { get; set; } = "general";
  private List<Role> Roles { get; set; }
  private int NavBarOptionSelection { get; set; }
  
   private void SetNavBarOptionSelection(int selection) {
    NavBarOptionSelection = selection;

    switch (selection) {
      case 0:
        MenuSelection = "general";
        break;
      case 1:
        MenuSelection = "permissions";
        break;
      case 2:
        MenuSelection = "members";
        break;
    }
  }

  protected override async Task OnInitializedAsync() {
    _rolesHubManager.RolesUpdatedHandler(roles => {
      Roles = roles;
      StateHasChanged();
    });

    await _rolesHubManager.SendRolesUpdatedAsync(_userContext.User.InstitutionId.ToString());
  }
  
  private void GoBackRoles() {
    _roleContext.SetSelection("roles");
  }
}