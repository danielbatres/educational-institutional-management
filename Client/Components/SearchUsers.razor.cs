using edu_institutional_management.Shared.Models;
using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace edu_institutional_management.Client.Components;

public partial class SearchUsers {
  private List<User> UsersNoInstitution { get; set; } = new();
  private List<User> UsersNoInstitutionFiltered { get; set; } = new();
  [Inject]
  private SettingsContext SettingsContext { get; set; }
  [Inject]
  private IUserService _userService { get; set; }
  private int NavBarOptionSelection { get; set; }
  private string SearchValue { get; set; } = string.Empty;
  private bool Loading { get; set; } = true;

  protected override async Task OnInitializedAsync() {
    UsersNoInstitution = await _userService.GetNoInstitutionUsers();
    UsersNoInstitutionFiltered = UsersNoInstitution.ToList();

    await Task.Delay(500);
    Loading = false;
  }

  private void SearchUsersList(KeyboardEventArgs e) {
    if (e.Key == "Enter") {
      Search();
    }
  }

  private void Search() {
    if (!string.IsNullOrEmpty(SearchValue)) {
      UsersNoInstitutionFiltered = UsersNoInstitution.Where(u =>
        u.Name.ToLower().Contains(SearchValue.ToLower()) ||
        u.LastName.ToLower().Contains(SearchValue.ToLower()) || u.Register.Email.ToLower().Contains(SearchValue.ToLower()) || u.UserName.ToLower().Contains(SearchValue.ToLower())).ToList();
    } else {
      UsersNoInstitutionFiltered = UsersNoInstitution.ToList();
    }
  }

  private void UpdateSearchValue(ChangeEventArgs e) {
    SearchValue = e.Value.ToString();
  }

  private void SetNavBarOptionSelection(int selection) {
    NavBarOptionSelection = selection;
  }
}