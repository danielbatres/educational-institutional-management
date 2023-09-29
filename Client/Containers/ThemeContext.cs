using edu_institutional_management.Shared.Models;
using edu_institutional_management.Client.Models;

namespace edu_institutional_management.Client.Containers;

public class ThemeContext : BaseContainer {
  public Palette LightTheme { get; set; } = new () {
    BackgroundColor = "#FFFFFF",
    LightBackgroundColor = "#f8f9fc",
    LightColor = "#ECECEC",
    TextColor = "#a3a5b5",
    LightTextColor = "#a3a4a5",
    DarkColor = "#0E1116",
    LightPrimaryColor = "#D9DFFF",
    FirstLoadingColor = "#f0f0f0",
    SecondLoadingColor = "#e0e0e0",
    ThirdLoadingColor = "#f0f0f0",
    SideBarColor = "#FFFFFF",
    SecondSideBarColor = "#FFFFFF"
  };
  public Palette DarkBarLightTheme { get; set; } = new() {
    BackgroundColor = "#FFFFFF",
    LightBackgroundColor = "#f8f9fc",
    LightColor = "#ECECEC",
    TextColor = "#a3a5b5",
    LightTextColor = "#a3a4a5",
    DarkColor = "#0E1116",
    LightPrimaryColor = "#D9DFFF",
    FirstLoadingColor = "#f0f0f0",
    SecondLoadingColor = "#e0e0e0",
    ThirdLoadingColor = "#f0f0f0",
    SideBarColor = "#101010",
    SecondSideBarColor = "#FFFFFF"
  };
  public Palette AmbientTheme { get; set; } = new() {
    BackgroundColor = "#FFFFFF",
    LightBackgroundColor = "hsla(111, 100%, 99%, 1);",
    LightColor = "#ECECEC",
    TextColor = "#a3a5b5",
    LightTextColor = "#a3a4a5",
    DarkColor = "#0E1116",
    LightPrimaryColor = "#D9DFFF",
    FirstLoadingColor = "#f0f0f0",
    SecondLoadingColor = "#e0e0e0",
    ThirdLoadingColor = "#f0f0f0",
    SideBarColor = "#FFFFFF",
    SecondSideBarColor = "#FFFFFF"
  };
  public Palette DarkTheme { get; set; } = new() {
    BackgroundColor = "#1b1c1e",
    LightBackgroundColor = "#161616",
    LightColor = "#2b2b2b",
    TextColor = "#868887",
    DarkColor = "#f8f9f9",
    LightPrimaryColor = "#292929",
    LightTextColor = "#75777d",
    SpecialLightColor = "#222",
    FirstLoadingColor = "#3c3c3c",
    SecondLoadingColor = "#242424",
    ThirdLoadingColor = "#3c3c3c",
    SideBarColor = "#101010",
    SecondSideBarColor = "#1b1c1e"
  };
  public Palette DarkerTheme { get; set; } = new() {
    BackgroundColor = "#101116",
    LightBackgroundColor = "#03020a",
    SpecialLightColor = "#22232b",
    TextColor = "#787a85",
    LightTextColor = "#535e77",
    LightColor = "#1e1f2b",
    DarkColor = "#dfdfe0",
    FirstLoadingColor = "#3c3c3c",
    SecondLoadingColor = "#242424",
    ThirdLoadingColor = "#3c3c3c",
    SideBarColor = "#101116",
    SecondSideBarColor = "#101116"
  };
  public string PrimaryColor { get; set; } = string.Empty;

  public Palette SelectedTheme { get; set; }

  public ThemeContext() {
    SelectedTheme = LightTheme;
  }

  public void SetPrimaryColor(string primary) {
    PrimaryColor = primary;

    NotifyStateChanged();
  }

  public void SetSelectedTheme(AppereanceSelection selectedTheme) {
    switch (selectedTheme) {
      case AppereanceSelection.LightTheme:
        SelectedTheme = LightTheme;
        break;
      case AppereanceSelection.DarkTheme:
        SelectedTheme = DarkTheme;
        break;
      case AppereanceSelection.DarkerTheme:
        SelectedTheme = DarkerTheme;
        break;
      case AppereanceSelection.AmbientTheme:
        SelectedTheme = AmbientTheme;
        break;
      case AppereanceSelection.DarkBarLightTheme:
        SelectedTheme = DarkBarLightTheme;
        break;
    }

    NotifyStateChanged();
  }
}