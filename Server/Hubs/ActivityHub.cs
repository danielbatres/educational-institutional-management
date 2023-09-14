using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace edu_institutional_management.Server.Hubs;

public class ActivityHub : MainHub {
  private readonly IActivityService _activityService;

  public ActivityHub(IActivityService activityService) {
    _activityService = activityService;
  }

  public async Task SendActivitiesUpdate(string groupName) {
    var activities = GetActivities();
    await Clients.Group(groupName).SendAsync("ActivityUpdated", activities);
  }

  public List<ActivityLog> GetActivities() {
    return _activityService.Get().ToList();
  }
}