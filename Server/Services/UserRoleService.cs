using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class UserRoleService : BaseService, IUserRoleService {
  public UserRoleService(ApplicationContextService applicationContextService) : base(applicationContextService) {}

  public IEnumerable<UserRole> Get() {
    return _applicationContext.UserRoles;
  }

  public async Task Create(UserRole userRole) {
    _applicationContext.UserRoles.Add(userRole);
  
    await _applicationContext.SaveChangesAsync();
  }
  
  public async Task Delete(Guid userId) {
    var originalRole = _applicationContext.UserRoles.FirstOrDefault(u => u.UserId.Equals(userId));
    
    if (originalRole != null) {
      _applicationContext.UserRoles.Remove(originalRole);

      await _applicationContext.SaveChangesAsync();
    }
  }
}

public interface IUserRoleService {
  IEnumerable<UserRole> Get();
  Task Create(UserRole userRole);
  Task Delete(Guid userRoleId);
}