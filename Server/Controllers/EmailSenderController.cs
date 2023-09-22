using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailSenderController : ControllerBase {
  private readonly IEmailSenderService _emailSenderService;

  public EmailSenderController(IEmailSenderService emailSenderService) {
    _emailSenderService = emailSenderService;
  }

  [HttpPost]
  [Route("send")]
  public async Task<IActionResult> SendEmail([FromBody] MailRequest mailRequest) {
    await _emailSenderService.SendEmail(mailRequest);

    return Ok();
  }
}