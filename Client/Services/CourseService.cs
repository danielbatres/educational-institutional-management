using System.Net.Http.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class CourseService : BaseService, ICourseService{
  public CourseService(HttpClient httpClient) : base (httpClient) { }
  
  public async Task Create(Course course) {
    var response = await HttpClient.PostAsJsonAsync("api/course/create", course);
    
    await CheckResponse(response);
  }
  
  public async Task Update(Course course) {
    var response = await HttpClient.PutAsJsonAsync("api/course/update", course);
    
    await CheckResponse(response);
  }
}

public interface ICourseService {
  Task Create(Course course);
  Task Update(Course course);
}