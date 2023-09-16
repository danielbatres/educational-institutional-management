using Microsoft.EntityFrameworkCore;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class RolePermissionService : BaseService, IRolePermissionService{
  public RolePermissionService(ApplicationContextService applicationContextService) : base(applicationContextService) {}
  
  public async Task Create(RolePermission rolePermission) {
    _applicationContext.RolePermissions.Add(rolePermission);
    
    await _applicationContext.SaveChangesAsync();
  }
  
  public async Task Delete(int rolePermission) {
    var originalRolePermission = _applicationContext.RolePermissions.Find(rolePermission);

    _applicationContext.Entry(originalRolePermission).State = EntityState.Deleted;

    await _applicationContext.SaveChangesAsync();
  }
}

public interface IRolePermissionService {
  Task Create(RolePermission rolePermission);
  Task Delete(int rolePermission);
}