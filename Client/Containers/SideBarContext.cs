namespace edu_institutional_management.Client.Containers;

public class SideBarContext : BaseContainer {
  public int SelectedOptionMainMenu { get; set; } = 0;
  public int SelectedOptionSettingsMenu { get; set; } = 0;

  public void SetSelectedOptionMainMenu(int option) {
    SelectedOptionMainMenu = option;

    NotifyStateChanged();
  }

  public void SetSelectedOptionSettingsMenu(int option) {
    SelectedOptionSettingsMenu = option;

    NotifyStateChanged();
  }
}