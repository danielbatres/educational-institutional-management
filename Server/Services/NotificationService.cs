using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class NotificationService : INotificationService {
  private readonly ApplicationContext _applicationContext;

  public NotificationService(ApplicationContextService applicationContextService)
  {
    var applicationContext = applicationContextService.GetApplicationContext();

    _applicationContext = applicationContext;
  }

  public async Task Create(Notification notification) {
    _applicationContext.Notifications.Add(notification);

    await _applicationContext.SaveChangesAsync();
  }

  public async Task Update(Notification notification) {
    _applicationContext.Notifications.Update(notification);

    await _applicationContext.SaveChangesAsync();
  }

  public IEnumerable<Notification> GetNotificationsByUserId(Guid userId) {
    return _applicationContext.Notifications.Where(n => n.UserId == userId);
  }
}

public interface INotificationService {
  Task Create(Notification notification);
  Task Update(Notification notification);
  IEnumerable<Notification> GetNotificationsByUserId(Guid userId);
}