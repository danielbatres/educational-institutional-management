using System.Data.Common;
using System.Linq.Expressions;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Containers;

public class RegisterInstitutionContext : BaseContainer {
  public Institution Institution { get; set; }
  [Inject]
  public IInstitutionService InstitutionService { get; set; }
  public uint Selection { get; set; } = (uint) RegisterInstitutionSelection.AddressInfo;
  public bool ShowRegisterModal { get; set; } = false;
  public List<User> InstitutionUsers { get; set; } = new();

  public void SetShowRegisterModal(bool showRegisterModal) {
    ShowRegisterModal = showRegisterModal;

    NotifyStateChanged();
  }

  public void CreateInstitution() {
    Institution = new() {
      Id = Guid.NewGuid(),
      Address = string.Empty,
      Email = string.Empty,
      Name = string.Empty,
      PhoneNumber = string.Empty,
      Country = string.Empty,
      IsActive = true,
      RegisteredDate = DateTime.Now
    };

    NotifyStateChanged();
  }

  public async Task SetInstitutionUsers() {
    if (!Institution.Id.Equals(null)) {
      InstitutionUsers = await InstitutionService.GetInstitutionUsers(Institution.Id);
    } else {
      InstitutionUsers = new();
    }
    
    NotifyStateChanged();
  }

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