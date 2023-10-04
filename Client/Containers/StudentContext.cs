using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Containers;

public class StudentContext : BaseContainer {
  public ActionType ActionStudent { get; set; }
  public Guid CurrentStudentId { get; set; }
  public Guid CurrentCategorySelectionId { get; set; }
  public bool CategoryCreation { get; set; }
  public Student Student { get; set; } = new();
  public List<FieldInformation> FieldInformations { get; set; } = new();
  public List<List<object>> Warnings { get; set; } = new() {
    new() { "", false },
    new() { "", false },
    new() { "", false },
    new() { "", false }
  };
  public int ErrorsCount { get; set; }

  public void SetWarnings(int index, List<object> options) {
    Warnings[index] = options;

    NotifyStateChanged();
  }

  public void SetErrorsCount(int count) {
    ErrorsCount = count;

    NotifyStateChanged();
  }

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
      UniqueIdentifier = $"{Guid.NewGuid()}",
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