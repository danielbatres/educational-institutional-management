using System.Net.Http.Json;
using edu_institutional_management.Client.Containers;

namespace edu_institutional_management.Client.Services;

public class StudentService : BaseService, IStudentService {
  private readonly StudentContext _studentContext;
  
  public StudentService(HttpClient httpClient, StudentContext studentContext) : base(httpClient) {
    _studentContext = studentContext;
  }
  
  public async Task Create() {
    var response = await HttpClient.PostAsJsonAsync("api/student/create", _studentContext.Student);
    
    await CheckResponse(response);
  }
  
  public async Task Update() {
    var response = await HttpClient.PutAsJsonAsync("api/student/update", _studentContext.Student);
    
    await CheckResponse(response);
  }
}

public interface IStudentService {
  Task Create();
  Task Update();
}