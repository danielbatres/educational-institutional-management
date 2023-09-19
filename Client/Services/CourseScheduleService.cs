using System.Net.Http.Json;
using System.Text.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class CourseScheduleService : BaseService, ICourseScheduleService {
  public CourseScheduleService(HttpClient httpClient) : base (httpClient) {}
  
  public async Task<List<CourseSchedule>> Get(Guid subjectCourseId) {
    var response = await HttpClient.GetAsync($"api/courseschedule?subjectCourseId={subjectCourseId}");
    
    var content = await CheckResponseContent(response);
    
    return JsonSerializer.Deserialize<List<CourseSchedule>>(content, JsonOptions) ?? new();
  }
  
  public async Task Create(CourseSchedule courseSchedule) {
    var response = await HttpClient.PostAsJsonAsync("api/courseschedule/create", courseSchedule);
    
    await CheckResponse(response);
  }
  
  public async Task Delete(Guid courseScheduleId) {
    var response = await HttpClient.DeleteAsync($"api/courseschedule/remove?courseScheduleId={courseScheduleId}");
    
    await CheckResponse(response);
  }
}

public interface ICourseScheduleService {
  Task<List<CourseSchedule>> Get(Guid subjectCourseId);
  Task Create(CourseSchedule courseSchedule);
  Task Delete(Guid courseScheduleId);
}