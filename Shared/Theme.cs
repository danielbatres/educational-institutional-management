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
    BackgroundColor = "#1f1f1f",
    LightBackgroundColor = "#141414",
    LightColor = "#38393d",
    TextColor = "#959595",
    DarkColor = "#FFFFFF",
    LightPrimaryColor = "#292929",
    LightTextColor = "#75777d",
  };
}