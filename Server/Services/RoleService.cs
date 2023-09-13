using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class RoleService : BaseService, IRoleService {

  public RoleService(ApplicationContextService applicationContextService) : base(applicationContextService) {}

  public async Task Create(Role role) {
    _applicationContext.Roles.Add(role);

    await _applicationContext.SaveChangesAsync();
  }

  public IEnumerable<Role> Get() {
    return _applicationContext.Roles.Include(r => r.Permissions);
  }
}

public interface IRoleService {
  Task Create(Role role);
  IEnumerable<Role> Get();
}