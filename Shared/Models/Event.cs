namespace edu_institutional_management.Shared.Models;

public class Event : MainDescription {
  public Guid Id { get; set; }
  public int ParticipantsCount { get; set; }
  public DateTime Date { get; set; }
}