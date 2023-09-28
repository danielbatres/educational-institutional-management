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
  [Inject]
  private Validators Validators { get; set; }
  [Inject]
  private StatusModalContext StatusModalContext { get; set; }
  private List<List<object>> Warnings { get; set; } = new() {
    new() { "", false }
  };
  private int ErrorsCount { get; set; }

  protected override void OnInitialized() {
    _roleContext.OnChange += HandleStateChange;

    AssignNewRol();
  }

  private async Task CreateNewRol() {
    ErrorsCount = 0;

    ValidateFields();

    if (!ErrorsCount.Equals(0)) {
      await StatusModalContext.SetStatus(StatusType.Danger);
      
      return; 
    }

    await RoleService.Create(Role);
    
    ActivityLog activity = new() {
      Author = $"{UserContext.User.Name} {UserContext.User.LastName}",
      UserName = UserContext.User.UserName,
      Date = DateTime.Now,
      Title = "Ha creado un nuevo rol",
      Message = $"Ha agregado un nuevo rol llamado: {Role.Name}",
      ActionType =  ActionType.Create
    };

    await _rolesHubManager.SendRolesUpdatedAsync(UserContext.User.InstitutionId.ToString() ?? string.Empty);
    await _activityService.Create(activity);
    ExitRoleCreation();

    await StatusModalContext.SetStatus(StatusType.Success);
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

  private void ValidateFields() {
    string warningValue = Validators.IsRequired(Role.Name ?? string.Empty);
    Warnings[0][0] = warningValue;

    if (!string.IsNullOrEmpty(warningValue)) {
      ErrorsCount++;
      Warnings[0][1] = true;
    } else {
      Warnings[0][1] = false;
    }
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