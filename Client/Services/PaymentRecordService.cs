using System.Net.Http.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class PaymentRecordService : BaseService, IPaymentRecordService {
  public PaymentRecordService(HttpClient httpClient) : base(httpClient) {}

  public async Task Create(PaymentRecord paymentRecord) {
    var response = await HttpClient.PostAsJsonAsync("api/paymentrecord/create", paymentRecord);

    await CheckResponse(response);
  }

  public async Task Update(PaymentRecord paymentRecord) {
    var response = await HttpClient.PutAsJsonAsync("api/paymentrecord/update", paymentRecord);

    await CheckResponse(response);
  }
}

public interface IPaymentRecordService {
  Task Create(PaymentRecord paymentRecord);
  Task Update(PaymentRecord paymentRecord);
}