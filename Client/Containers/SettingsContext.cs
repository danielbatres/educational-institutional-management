using edu_institutional_management.Shared.Models;
using edu_institutional_management.Client.Models;

namespace edu_institutional_management.Client.Containers;

public class SettingsContext : BaseContainer {
  public bool ShowSettingsModal { get; set; }
  public ShowSettingsOption ShowSettingsOption { get; set; }
  public User SelectedUser { get; set; } = new();

  public void SetShowSettingsModal(bool valueModal) {
    ShowSettingsModal = valueModal;

    NotifyStateChanged();
  }

  public void SetShowSettingsOption(ShowSettingsOption option) {
    ShowSettingsOption = option;

    NotifyStateChanged();
  }

  public void SetSelectedUser(User user) {
    SelectedUser = user;
    ShowSettingsOption = ShowSettingsOption.SendUserRequest;

    NotifyStateChanged();
  }
}
