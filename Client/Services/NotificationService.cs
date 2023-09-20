using System.Net.Http.Json;
using System.Text.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class NotificationService : BaseService, INotificationService {
  public NotificationService(HttpClient httpClient) : base (httpClient) {}

  public async Task<List<GeneralNotification>> Get() {
    var response = await HttpClient.GetAsync("api/generalnotification");

    var content = await CheckResponseContent(response);

    return JsonSerializer.Deserialize<List<GeneralNotification>>(content, JsonOptions) ?? new();
  }

  public async Task Create(GeneralNotification notification) {
    var response = await HttpClient.PostAsJsonAsync("api/generalnotification/create", notification);

    await CheckResponse(response);
  }

  public async Task Update(GeneralNotification notification) {
    var response = await HttpClient.PutAsJsonAsync("api/generalnotification/update", notification);

    await CheckResponse(response);
  } 
}

public interface INotificationService {
  Task<List<GeneralNotification>> Get();
  Task Create(GeneralNotification notification);
  Task Update(GeneralNotification notification);
}