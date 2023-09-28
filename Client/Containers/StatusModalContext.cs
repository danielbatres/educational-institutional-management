namespace edu_institutional_management.Client.Containers;

public class StatusModalContext : BaseContainer {
  public StatusType Status { get; set; }
  public bool ShowModal { get; set; }

  public async Task SetStatus(StatusType status) {
    Status = status;
    ShowModal = true;

    NotifyStateChanged();

    await Task.Delay(3000);

    ShowModal = false;

    NotifyStateChanged();
  }
}