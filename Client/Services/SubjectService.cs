using System.Net.Http.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class SubjectService : BaseService, ISubjectService {
  public SubjectService(HttpClient httpClient) : base (httpClient){}
  
  public async Task Create(Subject subject) {
    var response = await HttpClient.PostAsJsonAsync("api/subject/create", subject);
    
    await CheckResponse(response);
  }

  public async Task Update(Subject subject) {
     var response = await HttpClient.PutAsJsonAsync("api/subject/update", subject);
     
     await CheckResponse(response);
  }
}

public interface ISubjectService {
  Task Create(Subject subject);
  Task Update(Subject subject);
}