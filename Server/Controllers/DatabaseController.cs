using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

[ApiController]
[Route("/api/server")]
public class DatabaseController : ControllerBase {
  private readonly MainContext _mainContext;
  private readonly ApplicationContextService _applicationContextService;

  public DatabaseController(MainContext mainContext, ApplicationContextService applicationContextService) {
    _mainContext = mainContext;
    _applicationContextService = applicationContextService;
  }

  [HttpGet]
  [Route("db-connection")]
  public IActionResult SetDbConnection([FromBody] string dbName) {
    var connectionString = $"Data Source=DESKTOP-NMVIEF5\\SQLEXPRESS;Initial Catalog={dbName};Integrated security=True;TrustServerCertificate=True";
    _applicationContextService.ConfigureDynamicConnectionString(connectionString);

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
    using var applicationContext = _applicationContextService.GetApplicationContext();

    applicationContext.Database.EnsureCreated();

    return Ok();
  }
}