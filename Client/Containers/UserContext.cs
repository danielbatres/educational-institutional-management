using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Containers;

public class UserContext : BaseContainer {
  public User User { get; set; }
  public bool IsActiveUser { get; set; } = false;

  public void SetUser(User user) {
    User = user;

    NotifyStateChanged();
  }

  public void SetIsActiveUser(bool isActiveUser) {
    IsActiveUser = isActiveUser;

    NotifyStateChanged();
  }
}