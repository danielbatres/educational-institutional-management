namespace edu_institutional_management.Shared.Models;

public class ActivityLog {
  public Guid Id { get; set; }
  public string Title { get; set; }
  public string Message { get; set; }
  public string Author { get; set; }
  public string UserName { get; set; }
  public ActionType ActionType { get; set; }
  public DateTime Date { get; set; }
  public ICollection<ActivityLogView>? ActivityLogViews { get; set; }
}

public enum ActionType {
  Create,
  Update,
  Delete
}