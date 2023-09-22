using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class NotificationService : BaseService, INotificationService {
  public NotificationService(ApplicationContextService applicationContextService) : base(applicationContextService) {}

  public IEnumerable<Notification> Get() {
    return _applicationContext.Notifications;
  } 
 
  public async Task Create(Notification notification) {
    _applicationContext.Notifications.Add(notification);

    await _applicationContext.SaveChangesAsync();
  }

  public async Task Update(Notification notification) {
    var originalNotification = _applicationContext.Notifications.FirstOrDefault(n => n.Id.Equals(notification.Id));

    if (originalNotification != null) {
      _applicationContext.Entry(originalNotification).State = EntityState.Detached;
      _applicationContext.Entry(notification).State = EntityState.Modified;

      await _applicationContext.SaveChangesAsync();
    }
  }
}

public interface INotificationService {
  Task Create(Notification notification);
  Task Update(Notification notification);
  IEnumerable<Notification> Get();
}