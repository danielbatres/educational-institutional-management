using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentSettingsController : ControllerBase {
  private readonly IPaymentSettingsService _paymentSettingsService;

  public PaymentSettingsController(IPaymentSettingsService paymentSettingsService) {
    _paymentSettingsService = paymentSettingsService;
  }

  [HttpGet]
  public IActionResult Get() {
    return Ok(_paymentSettingsService.Get());
  }

  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Post([FromBody] PaymentSettings paymentSettings) {
    await _paymentSettingsService.Create(paymentSettings);

    return Ok();
  }

  [HttpPut]
  [Route("update")]
  public async Task<IActionResult> Put([FromBody] PaymentSettings paymentSettings) {
    await _paymentSettingsService.Update(paymentSettings);

    return Ok();
  }

  [HttpDelete]
  [Route("remove")]
  public async Task<IActionResult> Delete(string paymentSettingsId) {
    await _paymentSettingsService.Delete(Guid.Parse(paymentSettingsId));

    return Ok();
  }
}