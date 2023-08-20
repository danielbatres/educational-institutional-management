using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client;

public class UserContext {
  public User User { get; set; }
  public bool IsActiveUser { get; set; } = false;
  public event Action OnChange;

  public void SetIsActiveUser() {
    IsActiveUser = !IsActiveUser;

    NotifyStateChanged();
  }

  private void NotifyStateChanged() => OnChange?.Invoke();
}