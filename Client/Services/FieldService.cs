using System.Net.Http.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class FieldService : BaseService, IFieldService {
  public FieldService(HttpClient httpClient) : base(httpClient) { }

  public async Task Create(Field field) {
    var response = await HttpClient.PostAsJsonAsync("api/field/create", field);
  
    await CheckResponse(response);
  }
  
  public async Task Update(Field field) {
    var response = await HttpClient.PutAsJsonAsync("api/field/update", field);
  
    await CheckResponse(response);
  }
  
  public async Task Delete(Guid fieldId) {
    var response = await HttpClient.DeleteAsync($"api/field/remove?fieldId={fieldId}");
  
    await CheckResponse(response);
  }
}

public interface IFieldService {
  Task Create(Field field);
  Task Update(Field field);
  Task Delete(Guid fieldId);
}