using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Containers;

public class UserContext : BaseContainer {
  public User User { get; set; } 
  public bool IsActiveUser { get; set; } = false;
  private readonly NavigationManager _navigationManager;

  public UserContext(NavigationManager navigationManager) {
    _navigationManager = navigationManager;
  }

  public void SetUser(User user) {
    User = user;

    NotifyStateChanged();
  }

  public void NavigateToInstitution() {
    if (User.InstitutionId != null) {
      _navigationManager.NavigateTo($"/application/{User.InstitutionId}");
    }
  }

  public void SetIsActiveUser(bool isActiveUser) {
    IsActiveUser = isActiveUser;

    NotifyStateChanged();
  }
}