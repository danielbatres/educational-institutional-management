using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Client.Containers;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

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
  private List<Category> _categories = new();
  private int NavigationOption { get; set; } = 0;
  [Inject]
  private StatusModalContext StatusModalContext { get; set; }
  private List<Category> Categories {
    get => _categories;
    set {
      if (!value.SequenceEqual(_categories)) {
        _categories = value.ToList();

        HandleCategoriesChangeAsync();
      }
    }
  }

  private List<Field> Fields { get; set; } = new();

  private void SetNavigationOption(Guid selectedCategoryId, int option) {
    NavigationOption = option;
    _studentContext.SetCurrentCategorySelectionId(selectedCategoryId);
  }

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

    StatusModalContext.SetAcceptWarning(false);
    StatusModalContext.SetShowWarning(false); 
  }
  
  private async Task HandleCategoriesChangeAsync() {
    _studentContext.FieldInformations.Clear();
    if (_studentContext.ActionStudent.Equals(ActionType.Update)) {
      _studentContext.FieldInformations = await _fieldInformationService.Get(_studentContext.Student.Id);
    }

    foreach (var category in Categories ) {
      if (category.Fields == null) continue;

      foreach (var field in category.Fields) {
        if (!_studentContext.FieldInformations.Any(f => f.FieldId.Equals(field.Id))) {
          FieldInformation fieldInformation = new() {
            Information = string.Empty,
            StudentId = _studentContext.Student.Id,
            FieldId = field.Id
          };

          _studentContext.FieldInformations.Add(fieldInformation);
        }

        if (field.FieldType != FieldType.List) continue;
        field.Options = await _optionService.Get(field.Id);
      }
    }
  }

  private void HandleStateChange() {
    StateHasChanged();
  }

  private void ExitStudentView() {
    _navigationManager.NavigateTo($"/application/{_userContext.User.InstitutionId}/students");
  }
  
  private async Task SaveChanges() {
    _studentContext.ValidateFields();

    if (_studentContext.ErrorsCount != 0) {
      await StatusModalContext.SetStatus(StatusType.Danger);
      return;
    }
    
    if (_studentContext.ActionStudent.Equals(ActionType.Create)) {
      await CreateNewStudent();
    } else if (_studentContext.ActionStudent.Equals(ActionType.Update)) {
      await UpdateStudent();
    }

    await StatusModalContext.SetStatus(StatusType.Success);
  }
  
  private void RemoveChanges() {
    if (_studentContext.ActionStudent.Equals(ActionType.Create)) {
      ExitStudentView();
    }
  }

  private async Task OnInputFileChange(InputFileChangeEventArgs e) {
    var imageFile = await e.File.RequestImageFileAsync("image/png", 1920, 1920);
    var imageBytes = new byte[imageFile.Size];
    await imageFile.OpenReadStream().ReadAsync(imageBytes);

    _studentContext.Student.Photo = imageBytes;
    await SaveChanges();
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

    List<FieldInformation> fieldsInformationBackup = await _fieldInformationService.Get(_studentContext.Student.Id);
    
    foreach (var fieldInformation in _studentContext.FieldInformations) {
      if (fieldsInformationBackup.Any(fb => fb.FieldId.Equals(fieldInformation.FieldId))) {
        await _fieldInformationService.Update(fieldInformation);
      } else {
        await _fieldInformationService.Create(fieldInformation);
      }
    }

    await _studentHubManager.SendStudentsUpdatedAsync(_userContext.User.InstitutionId.ToString() ?? string.Empty);
    ExitStudentView();
  }
}