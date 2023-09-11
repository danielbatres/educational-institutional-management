using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class MembershipRequestController : ControllerBase {
  private readonly IMembershipRequestService _membershipRequestService;

  public MembershipRequestController(IMembershipRequestService membershipRequestService) {
    _membershipRequestService = membershipRequestService;
  }

  [HttpPost]
  [Route("create")]
  public async Task<IActionResult> Post([FromBody] MembershipRequest request) {
    await _membershipRequestService.Create(request);

    return Ok();
  }
}