using edu_institutional_management.Shared.Models;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Services;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Components;

public partial class StudentsSettings {
  [Inject]
  private CategoryHubManager _categoryHubManager { get; set; }
  [Inject]
  private UserContext _userContext { get; set; }
  [Inject]
  private StudentContext _studentContext { get; set; }
  [Inject]
  private StudentSettingsHubManager _studentSettingsHubManager { get; set; }
  [Inject]
  private IStudentSettingsService _studentSettingsService { get; set; }
  private StudentSettings StudentSettings { get; set; } = new();
  private List<Category> Categories { get; set; } = new();

  protected override async Task OnInitializedAsync() {
    _studentSettingsHubManager.StudentSettingsUpdatedHandler(studentSettings => {
      StudentSettings = studentSettings;
      StateHasChanged();
    });

    _categoryHubManager.CategoriesUpdatedHandler(categories => {
      Categories = categories;
      StateHasChanged();
    });

    string groupName = _userContext.User.InstitutionId.ToString() ?? string.Empty;

    await _categoryHubManager.SendCategoriesUpdatedAsync(groupName);
    await _studentSettingsHubManager.SendStudentSettingsUpdatedAsync(groupName);
  }

  private async Task UpdateIdentifier() {
    StudentSettings.DefaultIdentifier = !StudentSettings.DefaultIdentifier;

    await _studentSettingsService.Update(StudentSettings);
    await _studentSettingsHubManager.SendStudentSettingsUpdatedAsync(_userContext.User.InstitutionId.ToString() ?? string.Empty);
  }
}