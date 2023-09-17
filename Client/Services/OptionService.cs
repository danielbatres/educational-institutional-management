using System.Net.Http.Json;
using System.Text.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class OptionService : BaseService, IOptionService {
  public OptionService(HttpClient httpClient) : base(httpClient) {}
  
  public async Task<List<Option>> Get(Guid fieldId) {
    var response = await HttpClient.GetAsync($"api/option?fieldId={fieldId}");
    
    var content = await CheckResponseContent(response);
    
    return JsonSerializer.Deserialize<List<Option>>(content, JsonOptions) ?? new(); 
  }
  
  public async Task Create(Option option) {
    var response = await HttpClient.PostAsJsonAsync("api/option/create", option);
    
    await CheckResponse(response);
  }
  
  public async Task Update(Option option) {
    var response = await HttpClient.PutAsJsonAsync("api/option/update", option);
    
    await CheckResponse(response);
  }
  
  public async Task Delete(int optionId) {
    var response = await HttpClient.DeleteAsync($"api/option/remove?optionId={optionId}");
    
    await CheckResponse(response);
  }
}

public interface IOptionService {
  Task<List<Option>> Get(Guid fieldId);
  Task Create(Option option);
  Task Update(Option option);
  Task Delete(int optionId);
}