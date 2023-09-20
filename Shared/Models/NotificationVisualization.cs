using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class NotificationVisualization {
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public Guid NotificationId { get; set; }
    [JsonIgnore]
    public Notification Notification { get; set; }
}