namespace edu_institutional_management.Client.Containers;

public class BaseContainer {
  protected event Action OnChange;

  protected void NotifyStateChanged() => OnChange?.Invoke();
}