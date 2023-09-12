using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Client.Models;
using edu_institutional_management.Client.Services;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Layouts;

public partial class ApplicationLayout {
  [Inject]
  private ThemeContext _themeContext { get; set; }
  public bool Toggle { get; set; }
  private Menu SideBarMenu { get; set; } = new();
  [Inject]
  private UserContext _userContext { get; set; }
  [Inject]
  private RolesHubManager _rolesHubManager { get; set; }
  [Inject]
  private ISettingsService _settingsService { get; set; }
  [Inject]
  private GroupHubManager _groupHubManager { get; set; }
  public Guid InstitutionId { get; set; } = Guid.Empty;

  protected override void OnInitialized() {
    Toggle = true;

    InstitutionId = (Guid)_userContext.User.InstitutionId;

    SideBarMenu = new()
    {
      MenuLabels = new List<string>() {
        "menu principal", "menu de actividades"
      },
      MenuItems = new List<List<List<string>>>() {
        new() {
          new() {
            "Dashboard", "fi fi-tr-dice-d6", "0", $"/application/{InstitutionId}"
          },
          new() {
            "Estudiantes", "fi fi-tr-book-bookmark", "1", $"/application/{InstitutionId}/students"
          },
          new() {
            "Cursos", "fi fi-tr-hockey-puck", "2", $"/application/{InstitutionId}/courses"
          },
          new() {
            "Estadisticas", "fi fi-tr-chart-simple-horizontal", "3", $"/application/{InstitutionId}/statistics"
          }
        },
        new() {
          new() {
            "Eventos", "fi fi-tr-calendar-day", "4", $"/application/{InstitutionId}/events"
          },
          new() {
            "Actividad", "fi fi-tr-book-copy", "5", $"/application/{InstitutionId}/activity"
          }
        }
      },
      LastOption = new() {
        "Configuraciones", "fi fi-tr-gears", "6", $"/application/{InstitutionId}/settings/my-account"
      }
    };

    _themeContext.OnChange += HandleStateChange;
  }

  private void HandleStateChange() {
    StateHasChanged();
  }

  protected override async Task OnInitializedAsync() {
    await _groupHubManager.StartConnectionAsync();
    await _groupHubManager.JoinGroup(_userContext.User.Institution.Name);

    _userContext.User.Settings = await _settingsService.GetSettingsByUserId(_userContext.User.Id);

    _themeContext.SetSelectedTheme(_userContext.User.Settings.Appearance.Theme);
  }
}