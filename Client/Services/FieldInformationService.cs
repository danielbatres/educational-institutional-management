using System.Net.Http.Json;
using System.Text.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class FieldInformationService : BaseService, IFieldInformationService {
  public FieldInformationService(HttpClient httpClient) : base(httpClient) {}

  public async Task<List<FieldInformation>> Get(Guid studentId) {
    var response = await HttpClient.GetAsync($"api/fieldinformation?studentId={studentId}");

    var content = await CheckResponseContent(response);

    return JsonSerializer.Deserialize<List<FieldInformation>>(content, JsonOptions) ?? new();
  }

  public async Task Create(FieldInformation fieldInformation) {
    var response = await HttpClient.PostAsJsonAsync("api/fieldinformation/create", fieldInformation);
    
    await CheckResponse(response);
  }
  
  public async Task Update(FieldInformation fieldInformation) {
    var response = await HttpClient.PutAsJsonAsync("api/fieldinformation/update", fieldInformation);
    
    await CheckResponse(response);
  }
}

public interface IFieldInformationService {
  Task<List<FieldInformation>> Get(Guid studentId);
  Task Create(FieldInformation fieldInformation);
  Task Update(FieldInformation fieldInformation);
}