using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace edu_institutional_management.Server.Hubs;

public class CourseHub : MainHub {
  private readonly ICourseService _courseService;

  public CourseHub(ICourseService courseService) {
    _courseService = courseService;
  }
  
  public async Task SendCoursesUpdate(string groupName) {
    var courses = GetCourses;
    
    await Clients.Group(groupName).SendAsync("CoursesUpdated", courses);
  }
  
  public List<Course> GetCourses() {
    return _courseService.Get().ToList();
  }
}