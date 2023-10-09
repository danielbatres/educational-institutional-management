using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Containers;

public class PaymentSettingsContext : BaseContainer {
  public PaymentSettings PaymentSettings { get; set; } = new();

  public void SetNewPaymentSettings() {
    PaymentSettings = new() {
      PaymentName = string.Empty,
      PaymentDescription = string.Empty,
      Amount = 0,
      FrequencyMonths = 0,
      DueDate = DateTime.Now,
      IsRecurring = false,
      Id = Guid.NewGuid()
    };

    NotifyStateChanged();
  }

  public void SetPaymentSettings(PaymentSettings paymentSettings) {
    PaymentSettings = paymentSettings;

    NotifyStateChanged();
  }
}