namespace edu_institutional_management.Shared.Models;

public class Settings {
  public Guid Id { get; set; }
  public Guid UserId { get; set; }
  public int AppearanceId { get; set; }
  public Appearance? Appearance { get; set; }
}