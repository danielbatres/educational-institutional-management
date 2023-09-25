namespace edu_institutional_management.Client.Containers;

public class ProfileContext : BaseContainer {
  public int SelectedContainer { get; set; } = 1;

  public void SetSelectedContainer(int selectedContainer) {
    SelectedContainer = selectedContainer;

    NotifyStateChanged();
  }
}