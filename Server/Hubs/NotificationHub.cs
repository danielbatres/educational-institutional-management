using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.SignalR;

public class NotificationHub : Hub {
  private readonly INotificationService _notificationService;

  public NotificationHub(INotificationService notificationService) {
    _notificationService = notificationService;
  }

  public async Task JoinNotificationsGroup(string userId) {
    await Groups.AddToGroupAsync(Context.ConnectionId, userId);
  }

  public async Task SendNotificationsUpdate(string userId) {
    List<Notification> notifications;
    
    try { 
      notifications = GetNotifications(Guid.Parse(userId));
    } catch { return; }

    await Clients.Group(userId).SendAsync("NotificationsUpdated", notifications);
  }

  public List<Notification> GetNotifications(Guid userId) {
    return _notificationService.GetNotificationsByUserId(userId).ToList();
  }
}