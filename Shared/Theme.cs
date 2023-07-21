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
    PrimaryColor = "#4361EE",
    BackgroundColor = "#0F1112",
    LightBackgroundColor = "#1E1E1E",
    LightColor = "#2C2C2C",
    TextColor = "#828A91",
    DarkColor = "#FFFFFF",
    LightPrimaryColor = "#4361EE"
  };
}