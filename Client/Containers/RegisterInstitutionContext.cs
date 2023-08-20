using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Containers;

public class RegisterInstitutionContext : BaseContainer {
  public Institution Institution { get; set; }
  public uint Selection { get; set; } = (uint) RegisterInstitutionSelection.AddressInfo;

  public void SetSelection(RegisterInstitutionSelection selection) {
    Selection = (uint) selection;

    NotifyStateChanged();
  }
}

public enum RegisterInstitutionSelection {
  AddressInfo = 0,
  ContactInfo = 1,
  PaymentDetails = 2
}