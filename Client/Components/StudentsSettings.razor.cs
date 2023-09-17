using edu_institutional_management.Shared.Models;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Client.Containers;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Components;

public partial class StudentsSettings {
  [Inject]
  private CategoryHubManager _categoryHubManager { get; set; }
  [Inject]
  private UserContext _userContext { get; set; }
  [Inject]
  private StudentContext _studentContext { get; set; }
  private List<Category> Categories { get; set; } = new();

  protected override async Task OnInitializedAsync() {
    _categoryHubManager.CategoriesUpdatedHandler(categories => {
      Categories = categories;
      StateHasChanged();
    });

    await _categoryHubManager.SendCategoriesUpdatedAsync(_userContext.User.InstitutionId.ToString());
  }
}