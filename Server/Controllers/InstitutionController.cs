using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class InstitutionController : ControllerBase {
    private readonly IInstitutionService _institutionService;

    public InstitutionController(IInstitutionService institutionService) {
        _institutionService = institutionService;
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Post([FromBody] Institution institution) {
        await _institutionService.Create(institution);

        return Ok();
    }
}