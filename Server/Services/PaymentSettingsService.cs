using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class PaymentSettingsService : BaseService, IPaymentSettingsService {
  public PaymentSettingsService(ApplicationContextService applicationContextService) : base(applicationContextService) {}

  public IEnumerable<PaymentSettings> Get() {
    return _applicationContext.PaymentsSettings;
  }

  public async Task Create(PaymentSettings paymentSettings) {
    _applicationContext.PaymentsSettings.Add(paymentSettings);

    await _applicationContext.SaveChangesAsync();
  }

  public async Task Update(PaymentSettings paymentSettings) {
    var originalPaymentSettings = _applicationContext.PaymentsSettings.FirstOrDefault(p => p.Id.Equals(paymentSettings.Id));

    if (originalPaymentSettings != null) {
      _applicationContext.Entry(originalPaymentSettings).State = EntityState.Detached;
      _applicationContext.Entry(paymentSettings).State = EntityState.Modified;

      await _applicationContext.SaveChangesAsync();
    }
  }

  public async Task Delete(Guid paymentSettingsId) {
    var originalPaymentSettings = _applicationContext.PaymentsSettings.FirstOrDefault(p => p.Id.Equals(paymentSettingsId));

    if (originalPaymentSettings != null) {
      _applicationContext.PaymentsSettings.Remove(originalPaymentSettings);

      await _applicationContext.SaveChangesAsync();
    }
  }
}

public interface IPaymentSettingsService {
  IEnumerable<PaymentSettings> Get();
  Task Create(PaymentSettings paymentSettings);
  Task Update(PaymentSettings paymentSettings);
  Task Delete(Guid paymentSettingsId);
}