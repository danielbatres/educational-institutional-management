namespace edu_institutional_management.Shared.Models;
public class Activity {
  public Guid Id { get; set; }
  public string Title { get; set; }
  public string Message { get; set; }
  public User Author { get; set; }
  public DateTime Date { get; set; }
}