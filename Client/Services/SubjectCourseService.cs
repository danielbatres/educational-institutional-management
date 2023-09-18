using System.Net.Http.Json;
using System.Text.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class SubjectCourseService : BaseService, ISubjectCourseService {
  public SubjectCourseService(HttpClient httpClient) : base (httpClient) {}
  
  public async Task<List<SubjectCourse>> Get(Guid courseId) {
    var response = await HttpClient.GetAsync($"api/subjectcourse?courseId={courseId}");
    
    var content = await CheckResponseContent(response);
    
    return JsonSerializer.Deserialize<List<SubjectCourse>>(content, JsonOptions) ?? new();
  }
  
  public async Task Create(SubjectCourse subjectCourse) {
    var response = await HttpClient.PostAsJsonAsync("api/subjectcourse/create", subjectCourse);
    
    await CheckResponse(response);
  }
  
  public async Task Delete(Guid subjectCourseId) {
    var response = await HttpClient.DeleteAsync($"api/subjectcourse/remove?subjectCourseId={subjectCourseId}");
    
    await CheckResponse(response);
  }
}

public interface ISubjectCourseService {
  Task<List<SubjectCourse>> Get(Guid courseId);
  Task Create(SubjectCourse subjectCourse);
  Task Delete(Guid subjectCourseId);
}