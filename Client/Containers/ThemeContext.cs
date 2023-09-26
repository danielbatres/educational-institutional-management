using edu_institutional_management.Shared.Models;
using edu_institutional_management.Client.Models;

namespace edu_institutional_management.Client.Containers;

public class ThemeContext : BaseContainer {
  public Palette LightTheme { get; set; } = new () {
    PrimaryColor = "#4361EE",
    BackgroundColor = "#FFFFFF",
    LightBackgroundColor = "#f8f9fc",
    LightColor = "#ECECEC",
    TextColor = "#a3a5b5",
    LightTextColor = "#a3a4a5",
    DarkColor = "#0E1116",
    LightPrimaryColor = "#D9DFFF",
    FirstLoadingColor = "#f0f0f0",
    SecondLoadingColor = "#e0e0e0",
    ThirdLoadingColor = "#f0f0f0"
  };
  public Palette DarkTheme { get; set; } = new() {
    PrimaryColor = "#4361EE",
    BackgroundColor = "#1f1f1f",
    LightBackgroundColor = "#141414",
    LightColor = "#38393d",
    TextColor = "#959595",
    DarkColor = "#FFFFFF",
    LightPrimaryColor = "#292929",
    LightTextColor = "#75777d",
    SpecialLightColor = "#242424",
    FirstLoadingColor = "#3c3c3c",
    SecondLoadingColor = "#242424",
    ThirdLoadingColor = "#3c3c3c"
  };

  public Palette SelectedTheme { get; set; }

  public ThemeContext() {
    SelectedTheme = LightTheme;
  }

  public void SetSelectedTheme(AppereanceSelection selectedTheme) {
    switch (selectedTheme) {
      case AppereanceSelection.LightTheme:
        SelectedTheme = LightTheme;
        break;
      case AppereanceSelection.DarkTheme:
        SelectedTheme = DarkTheme;
        break;
    }

    NotifyStateChanged();
  }
}