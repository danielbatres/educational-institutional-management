namespace edu_institutional_management.Server.Services;

public class BaseService {
  protected readonly ApplicationContext _applicationContext;
  
  public BaseService(ApplicationContextService applicationContextService) {
    var applicationContext = applicationContextService.GetApplicationContext();

    _applicationContext = applicationContext;
  }
}