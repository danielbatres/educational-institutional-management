using System.Net.Http.Json;
using edu_institutional_management.Client.Containers;
namespace edu_institutional_management.Client.Services;

public class CourseService : BaseService, ICourseService{
  private readonly CourseContext _courseContext;

  public CourseService(HttpClient httpClient, CourseContext courseContext) : base (httpClient) {
    _courseContext = courseContext;
  }
  
  public async Task Create() {
    var response = await HttpClient.PostAsJsonAsync("api/course/create", _courseContext.Course);
    
    await CheckResponse(response);
  }
  
  public async Task Update() {
    var response = await HttpClient.PutAsJsonAsync("api/course/update", _courseContext.Course);
    
    await CheckResponse(response);
  }
}

public interface ICourseService {
  Task Create();
  Task Update();
}