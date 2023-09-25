using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Containers;

public class PaymentContext : BaseContainer {
  public Payment Payment { get; set; } = new();

  public void SetPaymentPlan(int paymentTypeId) {
    Payment.PaymentTypeId = paymentTypeId;

    NotifyStateChanged();
  }

  public void SetNewPayment() {
    Payment = new() {
      Id = Guid.NewGuid(),
      RegisterDate = DateTime.Now,
      EndDate = DateTime.Now
    };

    NotifyStateChanged();
  }
}