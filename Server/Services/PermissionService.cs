using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class PermissionService : BaseService, IPermissionService {
  public PermissionService(ApplicationContextService applicationContextService) : base(applicationContextService) { }

  public IEnumerable<Permission> Get() {
    return _applicationContext.Permissions;
  }

}

public interface IPermissionService {
  IEnumerable<Permission> Get();
}