using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace edu_institutional_management.Client.Hubs;

public class CategoryHubManager : HubManagerBase {
  public CategoryHubManager(NavigationManager navigationManager) {
    _hubConnection = new HubConnectionBuilder().WithUrl(navigationManager.ToAbsoluteUri("/categoryHub")).Build();
  }

  public void CategoriesUpdatedHandler(Action<List<Category>> handler) {
    _hubConnection.On("CategoriesUpdated", handler);
  }

  public async Task SendCategoriesUpdatedAsync(string groupName) {
    await _hubConnection.SendAsync("SendCategoriesUpdate", groupName);
  }
}