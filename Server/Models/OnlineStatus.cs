namespace edu_institutional_management.Server.Models;

public class OnlineStatus {
  public Guid Id { get; set; }
  public bool Status { get; set; }
  public DateTime LastConnection { get; set; }
  public User User { get; set; }
  public Guid UserId { get; set; }
}