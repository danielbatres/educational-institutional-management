namespace edu_institutional_management.Client.Containers;

public class LoadingContext : BaseContainer {
  public string LoadingMessage { get; set; } = string.Empty;
  public bool ShowLoading { get; set; }

  public void SetLoadingMessage(string message) {
    LoadingMessage = message;

    NotifyStateChanged();
  }

  public void SetLoading(bool showLoading) {
    ShowLoading = showLoading;

    NotifyStateChanged();
  }
}