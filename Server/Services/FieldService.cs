using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class FieldService : BaseService, IFieldService {
  public FieldService(ApplicationContextService applicationContextService) : base (applicationContextService) { }
  
  public async Task Create(Field field) {
    _applicationContext.Fields.Add(field);
    
    await _applicationContext.SaveChangesAsync();
  }
  
  public async Task Update(Field field) {
    var originalField = _applicationContext.Fields.FirstOrDefault(f => f.Id == field.Id);
    
    _applicationContext.Entry(originalField).State = EntityState.Detached;
    
    _applicationContext.Entry(field).State = EntityState.Modified;

    await _applicationContext.SaveChangesAsync();
  }
  
  public async Task Delete(Guid fieldId) {
    var originalField = _applicationContext.Fields.FirstOrDefault(f => f.Id == fieldId);

    if (originalField != null) {
      _applicationContext.Fields.Remove(originalField);
      await _applicationContext.SaveChangesAsync();
    }
  }
}

public interface IFieldService {
  Task Create(Field field);
  Task Update(Field field);
  Task Delete(Guid fieldId);
}