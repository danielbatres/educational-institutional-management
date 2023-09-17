using System.Net.Http.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class StudentSettingsService : BaseService, IStudentSettingsService {
  public StudentSettingsService(HttpClient httpClient) : base(httpClient) { }
  
  public async Task Update(StudentSettings studentSettings) {
    var response = await HttpClient.PutAsJsonAsync("api/studentsettings/update", studentSettings);
    
    await CheckResponse(response);
  }
}

public interface IStudentSettingsService {
  Task Update(StudentSettings studentSettings) ;
}