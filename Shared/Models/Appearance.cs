using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class Appearance {
  public int Id { get; set; }
  public AppereanceSelection Theme { get; set; }
  [JsonIgnore]
  public ICollection<Settings>? Settings { get; set; }
}

public enum AppereanceSelection {
  DarkBarLightTheme,
  LightTheme,
  AmbientTheme,
  DarkTheme,
  DarkerTheme
}