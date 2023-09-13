using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class FieldInformationService : BaseService, IFieldInformationService {
  public FieldInformationService(ApplicationContextService applicationContextService) : base(applicationContextService) {}

  public async Task Create(FieldInformation fieldInformation) {
    _applicationContext.FieldsInformation.Add(fieldInformation);
    
    await _applicationContext.SaveChangesAsync();
  }
  
  public async Task Update(FieldInformation fieldInformation) {
    _applicationContext.FieldsInformation.Add(fieldInformation);
    
    await _applicationContext.SaveChangesAsync();
  }
}

public interface IFieldInformationService {
  Task Create(FieldInformation fieldInformation);
  Task Update(FieldInformation fieldInformation);
}