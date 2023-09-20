using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class GeneralNotificationService : IGeneralNotificationService {
  private readonly ApplicationContext _applicationContext;

  public GeneralNotificationService(ApplicationContextService applicationContextService) {
    var applicationContext = applicationContextService.GetApplicationContext();

    _applicationContext = applicationContext;
  }

  public async Task Create(GeneralNotification notification) {
    _applicationContext.GeneralNotifications.Add(notification);

    await _applicationContext.SaveChangesAsync();
  }

  public async Task Update(GeneralNotification notification) {
    var originalGeneralNotification = _applicationContext.GeneralNotifications.FirstOrDefault(n => n.Id.Equals(notification.Id));

    if (originalGeneralNotification != null) {
      _applicationContext.Entry(originalGeneralNotification).State = EntityState.Detached;
      _applicationContext.Entry(notification).State = EntityState.Modified;

      await _applicationContext.SaveChangesAsync();
    }
  }

  public IEnumerable<GeneralNotification> Get() {
    return _applicationContext.GeneralNotifications;
  }
}

public interface IGeneralNotificationService {
  Task Create(GeneralNotification notification);
  Task Update(GeneralNotification notification);
  IEnumerable<GeneralNotification> Get();
}