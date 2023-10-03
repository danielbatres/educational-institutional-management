using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Client.Models;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Shared.Models;
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
  private StatusModalContext StatusModalContext { get; set; }
  [Inject]
  private GeneralSearchContext GeneralSearchContext { get; set; }
  public Guid InstitutionId { get; set; } = Guid.Empty;
  [Inject]
  private InstitutionHubManager _institutionHubManager { get; set; }
  [Inject]
  private ActivityHubManager ActivityHubManager { get; set; }
  private Institution Institution { get; set; } = new();
  private string ImageSrc { get; set; } = string.Empty;

  protected override void OnInitialized() {
    _themeContext.OnChange += HandleStateChange;
    Toggle = true;

    InstitutionId = (Guid)_userContext.User.InstitutionId;

    SideBarMenu = new() {
      MenuLabels = new List<string>() {
        "menu principal", "menu de actividades", "espacio de trabajo"
      },
      MenuItems = new List<List<List<string>>>() {
        new() {
          new() {
            "Dashboard", "fi fi-tr-dice-d6", "0", $"/application/{InstitutionId}", "0"
          },
          new() {
            "Estudiantes", "fi fi-tr-book-bookmark", "1", $"/application/{InstitutionId}/students", "0"
          },
          new() {
            "Cursos", "fi fi-tr-hockey-puck", "2", $"/application/{InstitutionId}/courses", "0"
          },
          new() {
            "Estadisticas", "fi fi-tr-chart-simple-horizontal", "3", $"/application/{InstitutionId}/statistics", "0"
          }
        },
        new() {
          new() {
            "Eventos", "fi fi-tr-calendar-day", "4", $"/application/{InstitutionId}/events", "0"
          },
          new() {
            "Actividad", "fi fi-tr-book-copy", "5", $"/application/{InstitutionId}/activity", "0"
          },
          new() {
            "Comunicación", "fi fi-tr-comments", "6", $"/application/{InstitutionId}/chat", "0"
          }
        },
        new() {
          new() {
            "Entorno", "fi fi-tr-notes", "7", $"/application/{InstitutionId}/workspace", "0"
          }
        }
      },
      LastOption = new() {
        "Configuraciones", "fi fi-tr-gears", "8", $"/application/{InstitutionId}/settings/my-account", "0"
      }
    };

    StatusModalContext.OnChange += HandleStateChange;
    GeneralSearchContext.OnChange += HandleStateChange;
  }

  private void HandleStateChange() {
    StateHasChanged();
  }

  protected override async Task OnInitializedAsync() {
    _institutionHubManager.InstitutionUpdatedHandler(institution => {
      Institution = institution;

      if (Institution.Photo != null) {
        var imageBase64 = Convert.ToBase64String(Institution.Photo);
        ImageSrc = $"data:image/png;base64,{imageBase64}";
      }
      
      StateHasChanged();
    });

    ActivityHubManager.ActivityUpdatedHandler(activities => {
      int activitiesCounter = 0;

      foreach (var activity in activities) {
        if (activity.ActivityLogViews != null) {
          if (!activity.ActivityLogViews.Any(a => a.UserId.Equals(_userContext.User.Id))) {
            activitiesCounter++;
          }
        } else {
          activitiesCounter++;
        }
      }

      SideBarMenu.MenuItems[1][1][4] = activitiesCounter.ToString(); 
      StateHasChanged();
    });

    string groupName = _userContext.User.InstitutionId.ToString() ?? string.Empty;

    await _institutionHubManager.SendInstitutionUpdatedAsync(groupName);
    await ActivityHubManager.SendActivityUpdatedAsync(groupName);

    _userContext.User.Settings = await _settingsService.GetSettingsByUserId(_userContext.User.Id);

    _themeContext.SetPrimaryColor(_userContext.User.Settings.PrimaryColor);
    _themeContext.SetSelectedTheme(_userContext.User.Settings.Appearance.Theme);
  }

  private void RemoveAbsoluteContent() {
    if (!GeneralSearchContext.PreventClose) {
      if (GeneralSearchContext.ShowGeneralSearch) {
        GeneralSearchContext.SetShowGeneralSearch(false);
      }
    }

    GeneralSearchContext.SetPreventClose(false);
  }
}