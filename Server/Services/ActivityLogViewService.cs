using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class ActivityLogViewService : BaseService, IActivityLogViewService {
  public ActivityLogViewService(ApplicationContextService applicationContextService) : base(applicationContextService) {}

  public async Task Create(ActivityLogView activityLogView) {
    _applicationContext.ActivitiesLogView.Add(activityLogView);

    await _applicationContext.SaveChangesAsync();
  }

  public async Task Update(ActivityLogView activityLogView) {
    var originalActivityLogView = _applicationContext.ActivitiesLogView.FirstOrDefault(a => a.Id.Equals(activityLogView.Id));

    if (originalActivityLogView != null) {
      _applicationContext.Entry(originalActivityLogView).State = EntityState.Detached;
      _applicationContext.Entry(activityLogView).State = EntityState.Modified;

      await _applicationContext.SaveChangesAsync();
    }
  }
}

public interface IActivityLogViewService {
  Task Create(ActivityLogView activityLogView);
  Task Update(ActivityLogView activityLogView);
}