using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace edu_institutional_management.Server.Hubs;

public class NotificationHub : MainHub {
  private readonly IGeneralNotificationService _generalNotificationService;

  public NotificationHub(IGeneralNotificationService generalNotificationService) {
    _generalNotificationService = generalNotificationService;
  }

  public async Task SendNotificationsUpdate(string userId) {
    await Clients.Group(userId).SendAsync("NotificationsUpdated");
  }
}