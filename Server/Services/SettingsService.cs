using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class SettingsService : BaseService, ISettingsService {
  public SettingsService(ApplicationContextService applicationContextService) : base(applicationContextService) {}

  public async Task Create(Settings settings) {
    _applicationContext.Settings.Add(settings);

    await _applicationContext.SaveChangesAsync();
  }

  public async Task Update(Settings settings) {
    var originalSettings = _applicationContext.Settings.Find(settings.Id);

    _applicationContext.Entry(originalSettings).State = EntityState.Detached;

    _applicationContext.Entry(settings).State = EntityState.Modified;

    await _applicationContext.SaveChangesAsync();
  }

  public Settings? GetSettingsByUserId(Guid userId) {
    return _applicationContext.Settings.Where(s => s.UserId == userId).Include(s => s.Appearance).FirstOrDefault();
  }
}

public interface ISettingsService {
  Task Create(Settings settings);
  Task Update(Settings settings);
  Settings? GetSettingsByUserId(Guid userId);
}