using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class ApplicationContextService {
  private string _connectionString;

  public void ConfigureDynamicConnectionString(string connectionString) {
    _connectionString = connectionString;
  }

  private ApplicationContext? ApplicationContext { get; set; }

  public ApplicationContext GetApplicationContext() {
    if (ApplicationContext == null || ApplicationContext.Database.GetDbConnection().ConnectionString != _connectionString) {
      var options = new DbContextOptionsBuilder<ApplicationContext>().UseSqlServer(_connectionString).Options;

      ApplicationContext =  new ApplicationContext(options);
    }
    
    return ApplicationContext;
  }

  public Guid GetActualInstitutionId() {
    return Guid.NewGuid();
  }
}