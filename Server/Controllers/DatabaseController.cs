using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("/api/server")]
public class DatabaseController : ControllerBase {
  private readonly MainContext _mainContext;
  private ApplicationContextService _applicationContextService;

  public DatabaseController(MainContext mainContext, ApplicationContextService applicationContextService) {
    _mainContext = mainContext;
    _applicationContextService = applicationContextService;
  }

  [HttpPost]
  [Route("db-connection")]
  public IActionResult SetDbConnection([FromBody] DataBaseConnectionRequest request) {
    var connectionString = $"Data Source=DESKTOP-NMVIEF5\\SQLEXPRESS;Initial Catalog={request.DataBaseName};Integrated security=True;TrustServerCertificate=True";
    _applicationContextService.SetSavedConnectionString(connectionString);

    return Ok("Successful database connection integration");
  }

  [HttpGet]
  [Route("create-main-db")]
  public IActionResult CreateMainDataBase() {
    _mainContext.Database.EnsureCreated();

    return Ok();
  }

  [HttpGet]
  [Route("create-application-db")]
  public IActionResult CreateApplicationDataBase() {
    var connectionString = _applicationContextService.GetSavedConnectionString();
    var applicationContext = _applicationContextService.GetApplicationContext(connectionString);

    applicationContext.Database.EnsureCreated();

    return Ok();
  }
}