using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace edu_institutional_management.Client.Hubs;

public class PaymentSettingsHubManager : HubManagerBase {
  public PaymentSettingsHubManager(NavigationManager navigationManager) {
    _hubConnection = new HubConnectionBuilder().WithUrl(navigationManager.ToAbsoluteUri("/paymentSettingsHub")).Build();
  }

  public void PaymentSettingsUpdatedHandler(Action<List<PaymentSettings>> handler) {
    _hubConnection.On("PaymentSettingsUpdated", handler);
  }

  public async Task SendPaymentSettingsUpdatedAsync(string groupName) {
    await _hubConnection.SendAsync("SendPaymentSettingsUpdate", groupName);
  }
}