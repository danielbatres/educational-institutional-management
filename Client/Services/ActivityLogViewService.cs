using System.Net.Http.Json;
using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class ActivityLogViewService : BaseService, IActivityLogViewService {
  private readonly UserContext _userContext;
  private readonly ActivityHubManager _activityHubManager;

  public ActivityLogViewService(HttpClient httpClient, UserContext userContext, ActivityHubManager activityHubManager) : base(httpClient) {
    _userContext = userContext;
    _activityHubManager = activityHubManager;
  }

  public async Task Create(ActivityLogView activityLogView) {
    var response = await HttpClient.PostAsJsonAsync("api/activityview/create" , activityLogView);

    await CheckResponse(response);

    await _activityHubManager.SendActivityUpdatedAsync(_userContext.User.InstitutionId.ToString() ?? string.Empty);
  }

  public async Task Update(ActivityLogView activityLogView) {
    activityLogView.UserId = _userContext.User.Id;

    var response = await HttpClient.PutAsJsonAsync("api/activityview/update", activityLogView);

    await CheckResponse(response);

    await _activityHubManager.SendActivityUpdatedAsync(_userContext.User.InstitutionId.ToString() ?? string.Empty);
  }
} 

public interface IActivityLogViewService {
  Task Create(ActivityLogView activityLogView);
  Task Update(ActivityLogView activityLogView);
}