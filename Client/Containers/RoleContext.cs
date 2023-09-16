namespace edu_institutional_management.Client.Containers;

public class RoleContext : BaseContainer {
  public string Selection { get; set; }
  public int ActualRoleIdSelection { get; set; }
  public bool CreateNewRol { get; set; } = false;
  
  public void SetSelection(string selection) {
    Selection = selection;

    NotifyStateChanged();
  }

  public void SetRolCreation(bool create) {
    CreateNewRol = create;

    NotifyStateChanged();
  }

  public void SetActualRoleIdSelection() {
    
  }
}