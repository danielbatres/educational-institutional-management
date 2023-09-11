using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class ApplicationContextService {
  private readonly IConfiguration _configuration;
  private ApplicationContext _applicationContext;
  private string _savedConnectionString;

  public ApplicationContextService(IConfiguration configuration) {
    _configuration = configuration;
  }

  public ApplicationContext GetApplicationContext(string connectionString)
  {
    if (_applicationContext == null || _applicationContext.Database.GetDbConnection().ConnectionString != connectionString)
    {
      var options = new DbContextOptionsBuilder<ApplicationContext>()
          .UseSqlServer(connectionString)
          .Options;

      _applicationContext = new ApplicationContext(options);
    }

    return _applicationContext;
  }

  public string GetSavedConnectionString() {
    return _savedConnectionString;
  }

  public void SetSavedConnectionString(string connectionString)
  {
    _savedConnectionString = connectionString;
  }

  public Guid GetActualInstitutionId() {
    return Guid.NewGuid();
  }
}