using Microsoft.EntityFrameworkCore;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class RolePermissionService : BaseService, IRolePermissionService{
  public RolePermissionService(ApplicationContextService applicationContextService) : base(applicationContextService) {}
  
  public async Task Create(RolePermission rolePermission) {
    _applicationContext.RolePermissions.Add(rolePermission);
    
    await _applicationContext.SaveChangesAsync();
  }
  
  public async Task Delete(int rolePermissionId) {
    var originalRolePermission = _applicationContext.RolePermissions.FirstOrDefault(rp => rp.Id == rolePermissionId);
    
    if (originalRolePermission != null) {
      _applicationContext.RolePermissions.Remove(originalRolePermission);
      await _applicationContext.SaveChangesAsync();
    }
  }


  public IEnumerable<RolePermission> Get(Guid roleId) {
    return _applicationContext.RolePermissions.Where(r => r.RoleId == roleId);
  }
}

public interface IRolePermissionService {
  Task Create(RolePermission rolePermission);
  Task Delete(int rolePermissionId);
  IEnumerable<RolePermission> Get(Guid roleId);
}