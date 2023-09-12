using System.Net.Http.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class NotificationService : BaseService, INotificationService {
  public NotificationService(HttpClient httpClient) : base (httpClient) {}

  public async Task Create(Notification notification) {
    var response = await HttpClient.PostAsJsonAsync("api/notification/create", notification);

    await CheckResponse(response);
  }

  public async Task Update(Notification notification) {
    var response = await HttpClient.PutAsJsonAsync("api/notification/update", notification);

    await CheckResponse(response);
  } 
}

public interface INotificationService {
  Task Create(Notification notification);
  Task Update(Notification notification);
}