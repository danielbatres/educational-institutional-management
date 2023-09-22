namespace edu_institutional_management.Shared.Models;

public class MainNotification {
  public int Id { get; set; }
  public string Title { get; set; }
  public string Message { get; set; }
  public DateTime CreationDate { get; set; }
  public bool Read { get; set; }
}