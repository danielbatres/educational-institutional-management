using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Containers;

public class StudentContext : BaseContainer {
  public ActionType ActionStudent { get; set; }
  public Guid CurrentStudentId { get; set; }
  public bool CategoryCreation { get; set; }
  
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
}