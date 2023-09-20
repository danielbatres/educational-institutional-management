using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class NotificationVisualizationService : BaseService, INotificationVisualizationService {
    public NotificationVisualizationService(ApplicationContextService applicationContextService) : base (applicationContextService) {}

    public IEnumerable<NotificationVisualization> Get(Guid userId) {
        return _applicationContext.NotificationsVisualization.Where(n => n.UserId.Equals(userId));
    }

    public async Task Create(NotificationVisualization notificationVisualization) {
        _applicationContext.NotificationsVisualization.Add(notificationVisualization);

        await _applicationContext.SaveChangesAsync();
    }

    public async Task Update(NotificationVisualization notificationVisualization) {
        var originalNotificationVisualization = _applicationContext.NotificationsVisualization.FirstOrDefault(n => n.Id.Equals(notificationVisualization.Id));

        if (originalNotificationVisualization != null) {
            _applicationContext.Entry(originalNotificationVisualization).State = EntityState.Detached;
            _applicationContext.Entry(notificationVisualization).State = EntityState.Modified;
            
            await _applicationContext.SaveChangesAsync();
        }
    }
}

public interface INotificationVisualizationService {
    Task Create(NotificationVisualization notificationVisualization);
    Task Update(NotificationVisualization notificationVisualization);
    IEnumerable<NotificationVisualization> Get(Guid userId);
}