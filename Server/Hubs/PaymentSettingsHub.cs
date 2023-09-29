using edu_institutional_management.Server.Hubs;
using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace edu_institutional_management.Server.Hubs;

public class PaymentSettingsHub : MainHub {
  private readonly IPaymentSettingsService _paymentSettingsService;

  public PaymentSettingsHub(IPaymentSettingsService paymentSettingsService) {
    _paymentSettingsService = paymentSettingsService;
  }

  public async Task SendPaymentSettingsUpdate(string groupName) {
    var paymentSettings = GetPaymentSettings();

    await Clients.Group(groupName).SendAsync("PaymentSettingsUpdated", paymentSettings);
  }

  private List<PaymentSettings> GetPaymentSettings() {
    return _paymentSettingsService.Get().ToList();
  }
}