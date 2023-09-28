using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class RoleService : BaseService, IRoleService {
  public RoleService(ApplicationContextService applicationContextService) : base(applicationContextService) {}

  public async Task Create(Role role) {
    _applicationContext.Roles.Add(role);

    await _applicationContext.SaveChangesAsync();
  }

  public async Task Update(Role role) {
    var originalRole = _applicationContext.Roles.FirstOrDefault(r => r.Id.Equals(role.Id));

    if (originalRole != null) {
      _applicationContext.Entry(originalRole).State = EntityState.Detached;
      _applicationContext.Entry(role).State = EntityState.Modified;

      await _applicationContext.SaveChangesAsync();
    }
  }

  public IEnumerable<Role> Get() {
    return _applicationContext.Roles.Include(r => r.RolePermissions);
  }
}

public interface IRoleService {
  Task Create(Role role);
  Task Update(Role role);
  IEnumerable<Role> Get();
}