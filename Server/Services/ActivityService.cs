using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class ActivityService : IActivityService {
  private readonly ApplicationContext _applicationContext;

  public ActivityService(ApplicationContextService applicationContextService) {
    var applicationContext = applicationContextService.GetApplicationContext();

    _applicationContext = applicationContext;
  }

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