using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Client.Containers;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Components;

public partial class ShowStudent {
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
  [Inject]
  private StudentHubManager _studentHubManager { get; set; }
  [Inject]
  private IStudentService _studentService { get; set; }
  [Inject]
  private IFieldInformationService _fieldInformationService { get; set; }
  private List<Category> Categories { get; set; } = new();
  private List<Field> Fields { get; set; } = new();

  protected override async Task OnInitializedAsync() {
    _studentContext.OnChange += HandleStateChange;

    _categoryHubManager.CategoriesUpdatedHandler(categories => {
      Categories = categories;
      StateHasChanged();
    });

    await _categoryHubManager.SendCategoriesUpdatedAsync(_userContext.User.InstitutionId.ToString() ?? string.Empty);
    
    _studentContext.SetCurrentCategorySelectionId(Guid.Empty);

    if (_studentContext.ActionStudent.Equals(ActionType.Create)) {
      _studentContext.SetNewStudent();
    }
  }
  
  protected override async Task OnAfterRenderAsync(bool isFirstRender) {
    _studentContext.FieldInformations.Clear();
    
    foreach (var category in Categories ) {
      if (category.Fields == null) continue;
      
      foreach (var field in category.Fields) {
        FieldInformation fieldInformation = new() {
          Information = string.Empty,
          StudentId = _studentContext.Student.Id,
          FieldId = field.Id
        };

        _studentContext.FieldInformations.Add(fieldInformation);
        
        if (field.FieldType != FieldType.List) continue;
    
        field.Options = await _optionService.Get(field.Id);
      }
    }
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
  
  private async Task SaveChanges() {
    if (_studentContext.ActionStudent.Equals(ActionType.Create)) {
      await CreateNewStudent();
    } else {
      await UpdateStudent();
    }
  }
  
  private void RemoveChanges() {
    if (_studentContext.ActionStudent.Equals(ActionType.Create)) {
      ExitStudentView();
    }
  }
  
  private async Task CreateNewStudent() {
    await _studentService.Create();
    
    foreach (var fieldInformation in _studentContext.FieldInformations) {
      if (fieldInformation.Information.Equals(string.Empty)) {
        fieldInformation.Information = "No agregado";
      }
      
      await _fieldInformationService.Create(fieldInformation);
    }
    
    await _studentHubManager.SendStudentsUpdatedAsync(_userContext.User.InstitutionId.ToString() ?? string.Empty);
    
    ExitStudentView();
  }
  
  private async Task UpdateStudent() {
    await _studentService.Update();
    
    await _studentHubManager.SendStudentsUpdatedAsync(_userContext.User.InstitutionId.ToString() ?? string.Empty);
  }
}