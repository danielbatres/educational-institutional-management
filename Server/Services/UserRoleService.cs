using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class UserRoleService : BaseService, IUserRoleService{
  public UserRoleService(ApplicationContextService applicationContextService) : base(applicationContextService) {}

  public async Task Create(UserRole userRole) {
    _applicationContext.UserRoles.Add(userRole);
  
    await _applicationContext.SaveChangesAsync();
  }
  
  public async Task Delete(UserRole userRole) {
    _applicationContext.UserRoles.Remove(userRole);
  
    await _applicationContext.SaveChangesAsync();
  }
}

public interface IUserRoleService {
  Task Create(UserRole userRole);
  Task Delete(UserRole userRole);
}