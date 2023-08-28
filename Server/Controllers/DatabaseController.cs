using Microsoft.AspNetCore.Mvc;

namespace edu_institutional_management.Server.Controllers;

public class DatabaseController : ControllerBase {
  private readonly MainContext context;
  public DatabaseController(MainContext mainContext, ApplicationContext applicationContext) {
    context = mainContext;
  }
}