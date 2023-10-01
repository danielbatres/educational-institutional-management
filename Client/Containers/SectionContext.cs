using edu_institutional_management.Client.Models;

namespace edu_institutional_management.Client.Containers;

public class SectionContext : BaseContainer {
  public bool ShowSectionModal { get; set; }
  public ShowSectionOptions ShowSectionOption { get; set; }

  public void SetShowSectionModal(bool show) {
    ShowSectionModal = show;

    NotifyStateChanged();
  }

  public void SetShowSectionOption(ShowSectionOptions option) {
    ShowSectionOption = option;

    NotifyStateChanged();
  }
}