using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Client.Models;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Components;

public partial class Users {
  [Inject]
  private UserContext UserContext { get; set; }
  [Inject]
  private UsersHubManager UsersHubManager { get; set; }
  private int NavBarOptionSelection { get; set; }
  [Inject]
  private SettingsContext SettingsContext { get; set; }
  private string SearchValue { get; set; } = string.Empty;
  private List<User> UsersList { get; set; } = new();
  private bool IsBoxStyle { get; set; } = false;

  private void SearchUsers() {
    SettingsContext.SetShowSettingsModal(true);
    SettingsContext.SetShowSettingsOption(ShowSettingsOption.SearchUsers);
  }

  protected override async Task OnInitializedAsync() {
    UsersHubManager.UsersUpdatedHandler(users => {
      UsersList = users;
      StateHasChanged();
    });

    await UsersHubManager.SendUsersUpdatedAsync(UserContext.User.InstitutionId.ToString() ?? string.Empty);
  }

  private void SetNavBarOptionSelection(int selection) {
    NavBarOptionSelection = selection;
  }
}