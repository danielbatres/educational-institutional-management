using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Containers;

public class StudentContext : BaseContainer {
  public ActionType ActionStudent { get; set; }
  public Guid CurrentStudentId { get; set; }
  public Guid CurrentCategorySelectionId { get; set; }
  public bool CategoryCreation { get; set; }
  public Student Student { get; set; } = new();
  public List<FieldInformation> FieldInformations { get; set; } = new();

  public void SetActionType(ActionType action) {
    ActionStudent = action;
    
    NotifyStateChanged();
  }
  
  public void SetCurrentStudent(Guid studentId) {
    CurrentStudentId = studentId;
    
    NotifyStateChanged();
  }
  
  public void SetCategoryCreation(bool creation) {
    CategoryCreation = creation;
    
    NotifyStateChanged();
  }

  public void SetCurrentCategorySelectionId(Guid categoryId) {
    CurrentCategorySelectionId = categoryId;

    NotifyStateChanged();
  }

  public void SetStudent(Student student) {
    Student = student;

    NotifyStateChanged();
  }

  public void SetNewStudent() {
    Guid newStudentId = Guid.NewGuid();

    Student = new() {
      Id = newStudentId,
      UniqueIdentifier = string.Empty,
      Name = string.Empty,
      LastName = string.Empty,
      PhoneNumber = string.Empty,
      Gender = "Masculino",
      StudentRegister = new() {
        Id = Guid.NewGuid(),
        CreatedAt = DateTime.Now,
        Email = string.Empty,
        Password = string.Empty,
        StudentId = newStudentId
      }
    };

    NotifyStateChanged();
  }
}