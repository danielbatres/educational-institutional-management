using edu_institutional_management.Shared.Models;
using edu_institutional_management.Server.Services;
using Microsoft.AspNetCore.SignalR;

namespace edu_institutional_management.Server.Hubs;

public class StudentHub : MainHub {
  private readonly IStudentService _studentService;

  public StudentHub(IStudentService studentService) {
    _studentService = studentService;
  }
  
  public async Task SendStudentsUpdate(string groupName) {
    var students = GetStudents();
    await Clients.Group(groupName).SendAsync("StudentsUpdated", students);
  }
  
  public List<Student> GetStudents() {
    return _studentService.Get().ToList();
  }
}