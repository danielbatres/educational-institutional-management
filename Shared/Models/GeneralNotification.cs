namespace edu_institutional_management.Shared.Models;

public class GeneralNotification : MainNotification {
  public ICollection<NotificationVisualization> NotificationsVisualization { get; set; }
}