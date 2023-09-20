using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class UserRoleService : BaseService, IUserRoleService{
  public UserRoleService(ApplicationContextService applicationContextService) : base(applicationContextService) {}

  public async Task Create(UserRole userRole) {
    _applicationContext.UserRoles.Add(userRole);
  
    await _applicationContext.SaveChangesAsync();
  }
  
  public async Task Delete(Guid userRoleId) {
    var originalRole = _applicationContext.UserRoles.FirstOrDefault(u => u.Id.Equals(userRoleId));
    
    if (originalRole != null) {
      _applicationContext.UserRoles.Remove(originalRole);

      await _applicationContext.SaveChangesAsync();
    }
  }
}

public interface IUserRoleService {
  Task Create(UserRole userRole);
  Task Delete(UserRole userRole);
}