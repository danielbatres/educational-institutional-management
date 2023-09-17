using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Client.Containers;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Components;

public partial class Student {
  [Inject]
  private StudentContext _studentContext { get; set; }
  [Inject]
  private CategoryHubManager _categoryHubManager { get; set; }
  [Inject]
  private UserContext _userContext { get; set; }
  [Inject]
  private IOptionService _optionService { get; set; }
  [Inject]
  private NavigationManager _navigationManager { get; set; }
  private List<Category> Categories = new();

  protected override async Task OnInitializedAsync() {
    _studentContext.OnChange += HandleStateChange;

    _categoryHubManager.CategoriesUpdatedHandler(categories => {
      Categories = categories;
      StateHasChanged();
    });

    await _categoryHubManager.SendCategoriesUpdatedAsync(_userContext.User.InstitutionId.ToString() ?? string.Empty);

    _studentContext.SetCurrentCategorySelectionId(Guid.Empty);
  }

  private void HandleStateChange() {
    StateHasChanged();
  }

  private async Task<List<Option>> GetOptions(Guid fieldId) {
    var options = await _optionService.Get(fieldId);

    return options;
  }

  private void ExitStudentView() {
    _navigationManager.NavigateTo($"/application/{_userContext.User.InstitutionId}/students");
  }
}