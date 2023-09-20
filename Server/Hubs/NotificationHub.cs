using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace edu_institutional_management.Server.Hubs;

public class NotificationHub : MainHub {
  private readonly IGeneralNotificationService _generalNotificationService;

  public NotificationHub(INotificationService notificationService) {
    _generalNotificationService = generalNotificationService;
  }

  public async Task JoinNotificationsGroup(string userId) {
    await Groups.AddToGroupAsync(Context.ConnectionId, userId);
  }

  public async Task SendNotificationsUpdate(string userId) {
    List<GeneralNotification> notifications;
    
    try { 
      notifications = GetNotifications(Guid.Parse(userId));
    } catch { return; }

    await Clients.Group(userId).SendAsync("NotificationsUpdated", notifications);
  }

  public List<GeneralNotification> GetNotifications(Guid userId) {
    return _generalNotificationService.GetNotificationsByUserId(userId).ToList();
  }
}