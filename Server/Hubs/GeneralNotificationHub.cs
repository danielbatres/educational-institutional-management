using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace edu_institutional_management.Server.Hubs;

public class GeneralNotificationHub : MainHub {
  private readonly IGeneralNotificationService _generalNotificationService;

  public GeneralNotificationHub(IGeneralNotificationService generalNotificationService) {
    _generalNotificationService = generalNotificationService;
  }

  public async Task SendGeneralNotificationsUpdate(string groupName) {
    var generalNotifications = GetNotifications();

    await Clients.Group(groupName).SendAsync("GeneralNotificationsUpdated", generalNotifications);
  }

  public List<GeneralNotification> GetNotifications() {
    return _generalNotificationService.Get().ToList();
  }
}