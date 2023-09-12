using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class ApplicationContextService {
  private string _connectionString;

  public void ConfigureDynamicConnectionString(string connectionString) {
    _connectionString = connectionString;
  }

  public ApplicationContext GetApplicationContext() {
    var options = new DbContextOptionsBuilder<ApplicationContext>().UseSqlServer(_connectionString).Options;

    return new ApplicationContext(options);
  }

  public Guid GetActualInstitutionId() {
    return Guid.NewGuid();
  }
}