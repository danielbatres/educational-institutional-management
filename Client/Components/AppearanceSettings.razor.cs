using System.Runtime.CompilerServices;
using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Components;

public partial class AppearanceSettings {
  private int SelectedAppearance { get; set; }
  [Inject]
  private UserContext _userContext { get; set; }
  [Inject]
  private ThemeContext _themeContext { get; set; }
  [Inject]
  private ISettingsService _settingsService { get; set; }
  private List<string> PrimaryColors { get; set; } = new() {
    "#4361EE", "#dfb82e", "#e54664", "#9a3cc9"
  };

  protected override void OnInitialized() {
    SelectedAppearance = _userContext.User.Settings.AppearanceId;
    _themeContext.SetSelectedTheme(_userContext.User.Settings.Appearance.Theme);
    _userContext.OnChange += HandleStateChange;
    _themeContext.OnChange += HandleStateChange;
  }

  private async Task SetSelectedAppearance(int selection) {
    SelectedAppearance = selection;
    _userContext.User.Settings.AppearanceId = selection;
    
    switch (selection) {
      case 1:
        _themeContext.SetSelectedTheme(AppereanceSelection.LightTheme);
        break;
      case 2:
        _themeContext.SetSelectedTheme(AppereanceSelection.DarkTheme);
        break;
      case 3:
        _themeContext.SetSelectedTheme(AppereanceSelection.DarkerTheme);
        break;
      case 4:
        _themeContext.SetSelectedTheme(AppereanceSelection.AmbientTheme);
        break;
      case 5:
        _themeContext.SetSelectedTheme(AppereanceSelection.DarkBarLightTheme);
        break;
    }

    await UpdateSettings();
  }

  private async Task UpdateSettings() {
    await _settingsService.Update(_userContext.User.Settings);
    _userContext.User.Settings = await _settingsService.GetSettingsByUserId(_userContext.User.Id);
  }

  private async Task SetPrimaryColor(string color) {
    _userContext.User.Settings.PrimaryColor = color;
    _themeContext.SetPrimaryColor(color);

    await UpdateSettings();
  }

  private void HandleStateChange() {
    StateHasChanged();
  }
}