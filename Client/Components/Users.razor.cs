using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Client.Models;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace edu_institutional_management.Client.Components;

public partial class Users {
  [Inject]
  private UserContext UserContext { get; set; }
  [Inject]
  private UsersHubManager UsersHubManager { get; set; }
  [Inject]
  private SettingsContext SettingsContext { get; set; }
  private string SearchValue { get; set; } = string.Empty;
  private List<User> UsersList { get; set; } = new();
  private List<User> UsersListFiltered { get; set; } = new();
  private bool IsBoxStyle { get; set; } = false;
  private bool Loading { get; set; } = true;
  private int RenderCount { get; set; } = 0;

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
    
    await Task.Delay(400);
    Loading = false;
  }

  protected override void OnAfterRender(bool firstRender) {
    if (RenderCount.Equals(1)) {
      UsersListFiltered = UsersList.ToList();
    }

    RenderCount++;
  }

  private void SearchUsersList(KeyboardEventArgs e) {
    if (e.Key == "Enter") {
      Search();
    }
  }

  private void Search() {
    if (!string.IsNullOrEmpty(SearchValue)) {
      UsersListFiltered = UsersList.Where(u =>
        u.Name.ToLower().Contains(SearchValue.ToLower()) ||
        u.LastName.ToLower().Contains(SearchValue.ToLower()) || u.Register.Email.ToLower().Contains(SearchValue.ToLower()) || u.UserName.ToLower().Contains(SearchValue.ToLower())).ToList();
    } else {
      UsersListFiltered = UsersList.ToList();
    }
  }

  private void UpdateSearchValue(ChangeEventArgs e) {
    SearchValue = e.Value.ToString();
  }

  private void Member() {

  }
}