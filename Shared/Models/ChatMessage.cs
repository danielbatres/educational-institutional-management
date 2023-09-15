namespace edu_institutional_management.Shared.Models;

public class ChatMessage {
  public int Id { get; set; }
  public string Message { get; set; }
  public DateTime TimeStamp { get; set; }
  public Guid UserId { get; set; }
}