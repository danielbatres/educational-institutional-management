using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Components;

public partial class AddEvent {
  [Inject]
  private IEventService EventService { get; set; }
  [Inject]
  private StatusModalContext StatusModalContext { get; set; }
  [Inject]
  private IActivityService ActivityService { get; set; }
  [Inject]
  private UserContext UserContext { get; set; }
  [Inject]
  private IGeneralNotificationService GeneralNotificationService { get; set; }
  [Inject]
  private ActivityHubManager ActivityHubManager { get; set; }
  [Inject]
  private GeneralNotificationHubManager GeneralNotificationHubManager { get; set; }
  [Inject]
  private Validators Validators { get; set; }
  private Event Event { get; set; } = new();
  private bool IsInputError { get; set; }
  public string InputWarning { get; set; } = string.Empty;

  protected override void OnInitialized() {
    AssignNewEvent();
  }

  private void AssignNewEvent() {
    Event = new() {
      Id = Guid.NewGuid(),
      Title = string.Empty,
      Description = string.Empty
    };
  }

  private void UpdateEvent(ChangeEventArgs e, string option) {
    string value = e.Value.ToString() ?? string.Empty;

    switch (option) {
      case "title":
        Event.Title = value;
        break;
      case "description":
        Event.Description = value;
        break;
    }
  }

  private void ValidateField() {
    IsInputError = false;
    string warning = Validators.IsRequired(Event.Title);
    InputWarning = warning;

    if (!string.IsNullOrEmpty(warning)) {
      IsInputError = true;
    }
  }

  private async Task CreateEvent() {
    ValidateField();

    if (IsInputError) {
      await StatusModalContext.SetStatus(StatusType.Danger);
      
      return;
    }

    ActivityLog activity = new() {
      ActionType = ActionType.Create,
      Author = $"{UserContext.User.Name} {UserContext.User.LastName}",
      Message = "Un nuevo evento ha sido agregado lo puedes revisar en la sección de eventos y programas educativos",
      Title = "Ha agregado un nuevo evento",
      UserName = UserContext.User.UserName
    };

    GeneralNotification notification = new() {
      Title = "Un nuevo evento ha sido agregado",
      Message = "Visita la sección de eventos para visualizarlo",
      RedirectText = "eventos",
      Redirection = "events"
    };

    await SendUpdates(activity, notification);
    await StatusModalContext.SetStatus(StatusType.Success);
  }

  private async Task SendUpdates(ActivityLog activity, GeneralNotification notification) {
    await ActivityService.Create(activity);
    await GeneralNotificationService.Create(notification);

    string groupName = UserContext.User.InstitutionId.ToString() ?? string.Empty;

    await ActivityHubManager.SendActivityUpdatedAsync(groupName);
    await GeneralNotificationHubManager.SendGeneralNotificationUpdatedAsync(groupName);
  }
}