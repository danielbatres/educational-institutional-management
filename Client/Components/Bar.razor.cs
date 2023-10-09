using Microsoft.AspNetCore.Components;
using edu_institutional_management.Client.Models;
using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Shared.Models;
using edu_institutional_management.Client.Hubs;

namespace edu_institutional_management.Client.Components;

public partial class Bar {
  [Inject]
  private NavigationManager? NavigationManager { get; set; }
  [Inject]
  private SideBarContext SideBarContext { get; set; }
  [Parameter]
  [EditorRequired]
  public bool IsToggle { get; set; }
  [CascadingParameter(Name = "Toggle")]
  public bool Toggle { get; set; }
  [Parameter]
  [EditorRequired]
  public Menu Menu { get; set; } = new Menu();
  [Parameter]
  public string Styles { get; set; } = string.Empty;
  public string BackgroundColor { get; set; } = string.Empty;
  [Parameter]
  [EditorRequired]
  public string SideBarOption { get; set; } = string.Empty;
  [Inject]
  private IPermissionService PermissionService { get; set; }
  [Inject]
  private RolesHubManager RolesHubManager { get; set; }
  [Inject]
  private UserRoleHubManager UserRoleHubManager { get; set; }
  [Inject]
  private UserContext UserContext { get; set; }
  [Inject]
  private IRolePermissionService RolePermissionService { get; set; }
  private List<Role> Roles { get; set; } = new();
  private List<UserRole> UserRoles { get; set; } = new();
  private List<Permission> Permissions { get; set; } = new();
  
  protected override async Task OnInitializedAsync() {
    Permissions = await PermissionService.Get();
    
    RolesHubManager.RolesUpdatedHandler(async roles => {
      Roles = roles;

      foreach (var role in Roles) {
        role.RolePermissions = await RolePermissionService.Get(role.Id);
      }

      StateHasChanged();
    });

    UserRoleHubManager.UserRolesUpdatedHandler(userRoles => {
      UserRoles = userRoles;
      StateHasChanged();
    });

    string groupName = UserContext.User.InstitutionId.ToString() ?? string.Empty;

    await RolesHubManager.SendRolesUpdatedAsync(groupName);
    await UserRoleHubManager.SendUserRolesUpdatedAsync(groupName);
  }

  public void SetSelectedValues(string navigateTo, string selected) {
    switch (SideBarOption) {
      case "main-menu":
        SideBarContext.SetSelectedOptionMainMenu(int.Parse(selected));
        break;
      case "settings-menu":
        SideBarContext.SetSelectedOptionSettingsMenu(int.Parse(selected));
        break;
    }

    NavigationManager?.NavigateTo(navigateTo);
  }

  protected override void OnParametersSet() {
    switch (SideBarOption) {
      case "main-menu":
        BackgroundColor = "background: var(--side-bar-color)";
        break;
      case "settings-menu":
        BackgroundColor = "background: var(--second-side-bar-color)";
        break;
    }
  }

  protected override void OnInitialized() {
    SideBarContext.OnChange += HandleStateChange;
  }

  private string GetSelectedContainer() {
    string container = string.Empty;

    switch (SideBarOption) {
      case "main-menu":
        container = SideBarContext.SelectedOptionMainMenu.ToString();
        break;
      case "settings-menu":
        container = SideBarContext.SelectedOptionSettingsMenu.ToString();
        break;
    }

    return container;
  }

  private void HandleStateChange() {
    StateHasChanged();
  }
}