using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Components;

public partial class Appearance {
  private int SelectedAppearance { get; set; }
  [Inject]
  private UserContext _userContext { get; set; }
  [Inject]
  private ThemeContext _themeContext { get; set; }
  [Inject]
  private ISettingsService _settingsService { get; set; }

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
    }

    Console.WriteLine(selection);
    await _settingsService.Update(_userContext.User.Settings);
    Console.WriteLine(_userContext.User.Settings.AppearanceId);
    _userContext.User.Settings = await _settingsService.GetSettingsByUserId(_userContext.User.Id);
  }

  private void HandleStateChange() {
    StateHasChanged();
  }
}