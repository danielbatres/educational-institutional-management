using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Containers;

public class StudentContext : BaseContainer {
  public ActionType ActionStudent { get; set; }
  public Student CurrentStudent { get; set; }
  
  public void SetActionType(ActionType action) {
    ActionStudent = action;
    
    NotifyStateChanged();
  }
  
  public void SetCurrentStudent(Student student) {
    CurrentStudent = student;
    
    NotifyStateChanged();
  }
}