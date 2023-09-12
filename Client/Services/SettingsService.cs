using System.Net.Http.Json;
using System.Text.Json;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Services;

public class SettingsService : BaseService, ISettingsService {

  public SettingsService(HttpClient httpClient) : base(httpClient) { }
  public async Task Create(Settings settings) {
    var response = await HttpClient.PostAsJsonAsync("api/settings/create", settings);

    await CheckResponse(response);
  }

  public async Task Update(Settings settings) {
    var response = await HttpClient.PutAsJsonAsync("api/settings/update", settings);

    await CheckResponse(response);
  }

  public async Task<Settings?> GetSettingsByUserId(Guid userId) {
    var response = await HttpClient.GetAsync($"api/settings/get-by-user-id?userId={userId}");

    var content = await CheckResponseContent(response);

    return JsonSerializer.Deserialize<Settings>(content, JsonOptions);
  }
}

public interface ISettingsService {
  Task Create(Settings settings);
  Task Update(Settings settings);
  Task<Settings?> GetSettingsByUserId(Guid userId);
}