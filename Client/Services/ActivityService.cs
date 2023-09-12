using System.Net.Http.Json;
using edu_institutional_management.Shared.Models;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Client.Containers;

namespace edu_institutional_management.Client.Services;

public class ActivityService : BaseService, IActivityService {
  private readonly ActivityHubManager _activityHubManager;
  private readonly UserContext _userContext;

  public ActivityService(HttpClient httpClient, ActivityHubManager activityHubManager, UserContext userContext) : base(httpClient) {
    _activityHubManager = activityHubManager;
    _userContext = userContext;
  }

  public async Task Create(ActivityLog activity) {
    var response = await HttpClient.PostAsJsonAsync("api/activity/create", activity);

    await CheckResponse(response);
    
    await _activityHubManager.StartConnectionAsync();
    
    await _activityHubManager.SendActivityUpdatedAsync(_userContext.User.Institution.Name);
  }
}

public interface IActivityService {
  Task Create(ActivityLog activity);
}