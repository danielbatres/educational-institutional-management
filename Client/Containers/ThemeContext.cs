using edu_institutional_management.Shared.Models;
using edu_institutional_management.Client.Models;

namespace edu_institutional_management.Client.Containers;

public class ThemeContext : BaseContainer {
  public Palette LightTheme { get; set; } = new () {
    BackgroundColor = "#FFFFFF",
    LightBackgroundColor = "#FFFFFF",
    LightColor = "#ECECEC",
    TextColor = "#a3a5b5",
    LightTextColor = "#a3a4a5",
    DarkColor = "#0E1116",
    LightPrimaryColor = "#D9DFFF",
    FirstLoadingColor = "#f0f0f0",
    SecondLoadingColor = "#e0e0e0",
    ThirdLoadingColor = "#f0f0f0",
    SideBarColor = "#FFFFFF",
    SideBarLightColor = "#ECECEC",
    SecondSideBarColor = "#FFFFFF",
    SecondSideBarLightColor = "#ECECEC",
    LogoColor = "#0E1116"
  };
  public Palette DarkBarLightTheme { get; set; } = new() {
    BackgroundColor = "#FFFFFF",
    LightBackgroundColor = "#f2f3f5",
    LightColor = "#ECECEC",
    TextColor = "#a3a5b5",
    LightTextColor = "#a3a4a5",
    DarkColor = "#0E1116",
    LightPrimaryColor = "#D9DFFF",
    FirstLoadingColor = "#f0f0f0",
    SecondLoadingColor = "#e0e0e0",
    ThirdLoadingColor = "#f0f0f0",
    SideBarColor = "#101010",
    SideBarLightColor = "",
    SecondSideBarColor = "#FFFFFF",
    LogoColor = "#FFFFFF"
  };
  public Palette AmbientTheme { get; set; } = new() {
    BackgroundColor = "#FFFFFF",
    LightBackgroundColor = "#FFFFFF",
    LightColor = "#ECECEC",
    TextColor = "#76787d",
    LightTextColor = "#a3a4a5",
    DarkColor = "#0E1116",
    SpecialLightColor = "#e3e5e8",
    LightPrimaryColor = "#D9DFFF",
    FirstLoadingColor = "#f0f0f0",
    SecondLoadingColor = "#e0e0e0",
    ThirdLoadingColor = "#f0f0f0",
    SideBarColor = "#e3e5e8",
    SecondSideBarColor = "#f2f3f5",
    LogoColor = "#0E1116"
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
    SecondSideBarColor = "#1b1c1e",
    LogoColor = "#FFFFFF"
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
    SecondSideBarColor = "#101116",
    LogoColor = "#FFFFFF"
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