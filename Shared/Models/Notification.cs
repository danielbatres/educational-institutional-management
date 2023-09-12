namespace edu_institutional_management.Shared.Models;

public class Notification {
  public int Id { get; set; }
  public string Title { get; set; }
  public string Message { get; set; }
  public DateTime CreationDate { get; set; }
  public bool Read { get; set; }
  public Guid UserId { get; set; }
}