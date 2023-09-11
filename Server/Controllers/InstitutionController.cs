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

    [HttpGet]
    [Route("get-institution-by")]
    public IActionResult GetInstitutionId(string name) {
        return Ok(_institutionService.Get().Where(x => x.Name == name));
    }

    [HttpGet]
    [Route("get-institution-users")]
    public IActionResult GetInstitutionUsers(Guid institutionId) {
        return Ok(_institutionService.GetInstitutionUsers(institutionId));
    } 
}