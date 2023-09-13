namespace edu_institutional_management.Client.Containers;

public class LoadingSiteContext : BaseContainer {
  public bool IsLoading { get; set; } = true;
  
  public void SetLoading(bool loading) {
    IsLoading = loading;
    
    NotifyStateChanged();
  }
}