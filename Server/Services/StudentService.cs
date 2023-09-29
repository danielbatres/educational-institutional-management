using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class StudentService : BaseService, IStudentService {
  public StudentService(ApplicationContextService applicationContextService) : base (applicationContextService) {}
  
  public IEnumerable<Student> Get() {
    return _applicationContext.Students.Include(s => s.Attendances).Include(s => s.StudentRegister).Include(s => s.FieldsInformation).Include(s => s.Course).Include(s => s.PaymentRecords);
  }
  
  public async Task Create(Student student) {
    _applicationContext.Students.Add(student);
    
    await _applicationContext.SaveChangesAsync();
  }
  
  public async Task Update(Student student) {
    var originalStudent = _applicationContext.Students.Where(s => s.Id.Equals(student.Id)).Include(s => s.StudentRegister).FirstOrDefault();

    if (originalStudent != null) {
      _applicationContext.Entry(originalStudent).State = EntityState.Detached;
      _applicationContext.Entry(student).State = EntityState.Modified;
      _applicationContext.Entry(student.StudentRegister).State = EntityState.Modified;

      await _applicationContext.SaveChangesAsync();
    }
  }
}

public interface IStudentService {
  Task Create(Student student);
  Task Update(Student student);
  IEnumerable<Student> Get();
}