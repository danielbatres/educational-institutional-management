using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class SettingsService : ISettingsService {
  private readonly ApplicationContext _applicationContext;

  public SettingsService(ApplicationContextService applicationContext) {
    _applicationContext = applicationContext.GetApplicationContext(applicationContext.GetSavedConnectionString());
  }

  public async Task CreateOrUpdate(Settings settings, string option) {
    switch(option) {
      case "create":
        _applicationContext.Settings.Add(settings);
        break;
      case "update":
        _applicationContext.Settings.Update(settings);
        break;
    }

    await _applicationContext.SaveChangesAsync();
  }

  public Settings? GetSettingsByUserId(Guid userId) {
    return _applicationContext.Settings.Where(s => s.UserId == userId).FirstOrDefault();
  }
}

public interface ISettingsService {
  Task CreateOrUpdate(Settings settings, string option);
  Settings? GetSettingsByUserId(Guid userId);
}