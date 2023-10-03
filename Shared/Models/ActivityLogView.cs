using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class ActivityLogView {
  public Guid Id { get; set; }
  public Guid ActivityId { get; set; }
  [JsonIgnore]
  public ActivityLog? Activity { get; set; }
  public Guid UserId { get; set; }
}