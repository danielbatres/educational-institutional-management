using edu_institutional_management.Shared.Models;
using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Client.Hubs;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Components;

public partial class CreateRole {
  [Inject]
  private UserContext UserContext { get; set; }
  [Inject]
  private IRoleService RoleService { get; set; }
  [Inject]
  private RolesHubManager _rolesHubManager { get; set; }
  public Role Role { get; set; } = new();
  [Inject]
  private IActivityService _activityService { get; set; }
  [Inject]
  private RoleContext _roleContext { get; set; }
  [Inject]
  private SettingsContext SettingsContext { get; set; }

  protected override void OnInitialized() {
    _roleContext.OnChange += HandleStateChange;

    AssignNewRol();
  }

  private async Task CreateNewRol() {
    await RoleService.Create(Role);
    
    ActivityLog activity = new() {
      Author = $"{UserContext.User.Name} {UserContext.User.LastName}",
      UserName = UserContext.User.UserName,
      Date = DateTime.Now,
      Title = "Ha creado un nuevo rol",
      Message = $"Ha agregado un nuevo rol llamado: {Role.Name}",
      ActionType =  ActionType.Create
    };

    await _rolesHubManager.SendRolesUpdatedAsync(UserContext.User.InstitutionId.ToString());
    await _activityService.Create(activity);
    ExitRoleCreation();
  }

  private void AssignNewRol() {
    Role = new() {
      Id = Guid.NewGuid(),
      RoleColor = "#ffffff"
    };
  }

  private void ExitRoleCreation() {
    SettingsContext.SetShowSideForm(false);

    AssignNewRol();
  }

  private void Update(ChangeEventArgs e, string update) {
    string value = e.Value.ToString();

    switch (update) {
      case "name":
        Role.Name = value;
        break;
      case "description":
        Role.Description = value;
        break;
    }
  }

  private void HandleStateChange() {
    StateHasChanged();
  }
}