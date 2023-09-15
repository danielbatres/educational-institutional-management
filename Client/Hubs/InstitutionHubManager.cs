using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace edu_institutional_management.Client.Hubs;

public class InstitutionHubManager : HubManagerBase {
  public InstitutionHubManager(NavigationManager navigationManager) {
    _hubConnection = new HubConnectionBuilder().WithUrl(navigationManager.ToAbsoluteUri("/institutionHub")).Build();
  }

public void InstitutionUpdatedHandler(Action<Institution> handler) {
  _hubConnection.On("InstitutionUpdated", handler);
}

public async Task SendInstitutionUpdatedAsync(string groupName) {
  await _hubConnection.SendAsync("SendInstitutionUpdate", groupName);
}
}