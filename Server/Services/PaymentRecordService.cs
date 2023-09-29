using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class PaymentRecordService : BaseService, IPaymentRecordService {
  public PaymentRecordService(ApplicationContextService applicationContextService) : base(applicationContextService
  ) {}

  public async Task Create(PaymentRecord paymentRecord) {
    _applicationContext.PaymentsRecord.Add(paymentRecord);

    await _applicationContext.SaveChangesAsync();
  }

  public async Task Update(PaymentRecord paymentRecord) {
    var originalPaymentRecord = _applicationContext.PaymentsRecord.FirstOrDefault(p => p.Id.Equals(paymentRecord));

    if (originalPaymentRecord != null) {
      _applicationContext.Entry(originalPaymentRecord).State = EntityState.Detached;
      _applicationContext.Entry(paymentRecord).State = EntityState.Modified;

      await _applicationContext.SaveChangesAsync();
    }
  }
}

public interface IPaymentRecordService {
  Task Create(PaymentRecord paymentRecord);
  Task Update(PaymentRecord paymentRecord);
}