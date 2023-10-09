using edu_institutional_management.Shared.Models;
using edu_institutional_management.Client.Models;

namespace edu_institutional_management.Client.Containers;

public class SettingsContext : BaseContainer {
  public bool ShowSettingsModal { get; set; }
  public bool ShowSideForm { get; set; }
  public ShowSettingsOption ShowSettingsOption { get; set; }
  public ShowSideFormOptions  ShowSideFormOptions { get; set; }
  public User SelectedUser { get; set; } = new();
  public Subject SelectedSubject { get; set; } = new();

  public void SetShowSettingsModal(bool valueModal) {
    ShowSettingsModal = valueModal;

    NotifyStateChanged();
  }

  public void SetShowSideForm(bool newValue) {
    ShowSideForm = newValue;

    NotifyStateChanged();
  }

  public void SetShowSideFormOptions(ShowSideFormOptions showOptions) {
    ShowSideFormOptions = showOptions;

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

  public void SetSelectedSuject(Subject subject) {
    SelectedSubject = subject;

    NotifyStateChanged();
  }

  public void AssignNewSubject() {
    SelectedSubject = new() {
      Id = Guid.NewGuid(),
      Name = string.Empty,
      Description = string.Empty,
      Color = string.Empty
    };

    NotifyStateChanged();
  }
}
