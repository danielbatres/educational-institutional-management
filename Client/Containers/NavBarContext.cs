namespace edu_institutional_management.Client.Containers;

public class NavBarContext : BaseContainer {
  public bool HideButtons { get; set; } = false;

  public void ChangeHideButtons(bool newState) {
    HideButtons = newState;

    NotifyStateChanged();
  }
}