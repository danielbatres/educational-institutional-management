using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class ActivityService : BaseService, IActivityService {
  public ActivityService(ApplicationContextService applicationContextService) : base(applicationContextService) { }

  public async Task Create(ActivityLog activity) {
    _applicationContext.Activities.Add(activity);

    await _applicationContext.SaveChangesAsync();
  }

  public IEnumerable<ActivityLog> Get() {
    return _applicationContext.Activities;
  }
}

public interface IActivityService {
  Task Create(ActivityLog activity);
  IEnumerable<ActivityLog> Get();
}