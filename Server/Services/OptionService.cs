using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class OptionService : BaseService, IOptionService {
  public OptionService(ApplicationContextService applicationContextService) : base(applicationContextService) {}
  
  public IEnumerable<Option> Get(Guid fieldId) {
    return _applicationContext.Options.Where(o => o.FieldId == fieldId);
  }
  
  public async Task Create(Option option) {
    if (!_applicationContext.Options.Any(o => o.Id.Equals(option.Id))) {
      _applicationContext.Options.Add(option);

      await _applicationContext.SaveChangesAsync(); 
    }
  }
  
  public async Task Update(Option option) {
    var originalOption = _applicationContext.Options.FirstOrDefault(option);
    
    _applicationContext.Entry(originalOption).State = EntityState.Detached;
    
    _applicationContext.Entry(option).State = EntityState.Modified;
    
    await _applicationContext.SaveChangesAsync();
  }
  
  public async Task Delete(Guid optionId) {
    var originalOption = _applicationContext.Options.FirstOrDefault(o => o.Id == optionId);

    if (originalOption != null) {
      _applicationContext.Options.Remove(originalOption);
      await _applicationContext.SaveChangesAsync();
    }
  }
}

public interface IOptionService {
  IEnumerable<Option> Get(Guid fieldId);
  Task Create(Option option);
  Task Update(Option option);
  Task Delete(Guid optionId);
}