namespace edu_institutional_management.Shared;

public class Theme {
  public static Palette LightTheme { get; set; } = new () {
    PrimaryColor = "#4361EE",
    BackgroundColor = "#FAFAFA",
    LightBackgroundColor = "#FFFFFF",
    LightColor = "#ECECEC",
    TextColor = "#828A91",
    DarkColor = "#0E1116",
    LightPrimaryColor = "#D9DFFF"
  };
  public static Palette DarkTheme { get; set; } = new() {
    PrimaryColor = "#242424",
    BackgroundColor = "#262626",
    LightBackgroundColor = "#1c1c1c",
    LightColor = "#38393d",
    TextColor = "#959595",
    DarkColor = "#FFFFFF",
    LightPrimaryColor = "#292929"
  };
}