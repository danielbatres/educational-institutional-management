using System.Net.Http.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class PaymentSettingsService : BaseService, IPaymentSettingsService {
  public PaymentSettingsService(HttpClient httpClient) : base(httpClient) { }

  public async Task Create(PaymentSettings paymentSettings) {
    var response = await HttpClient.PostAsJsonAsync("api/paymentsettings/create", paymentSettings);

    await CheckResponse(response);
  }

  public async Task Update(PaymentSettings paymentSettings) {
    var response = await HttpClient.PutAsJsonAsync("api/paymentsettings/update", paymentSettings);

    await CheckResponse(response);
  }
}

public interface IPaymentSettingsService {
  Task Create(PaymentSettings paymentSettings);
  Task Update(PaymentSettings paymentSettings);
}