using Microsoft.AspNetCore.Components.Web.Extensions.Head;

namespace edu_institutional_management.Client.Containers;

public class StatusModalContext : BaseContainer {
  public StatusType Status { get; set; }
  public bool ShowModal { get; set; }
  public string WarningMessage { get; set; } = "¿Estas seguro que quieres continuar? aun tienes campos que llenar";
  public string WarningTitle { get; set; } = "Campos vacios";
  public bool AcceptWarning { get; set; }
  public bool ShowWarning { get; set; } 

  public async Task SetStatus(StatusType status) {
    Status = status;
    ShowModal = true;

    NotifyStateChanged();

    await Task.Delay(3000);

    ShowModal = false;

    NotifyStateChanged();
  }

  public void SetWarningMessage(string title, string message) {
    WarningTitle = title;
    WarningMessage = message;

    NotifyStateChanged();
  }

  public void SetAcceptWarning(bool accept) {
    AcceptWarning = accept;

    NotifyStateChanged();
  }

  public void SetShowWarning(bool show) {
    ShowWarning = show;

    NotifyStateChanged();
  }
}