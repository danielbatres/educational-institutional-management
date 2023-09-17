using edu_institutional_management.Client.Services;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Client.Containers;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Components;

public partial class CreateCategory {
  [Inject]
  private ICategoryService _categoryService { get; set; }
  [Inject]
  private IFieldService _fieldService { get; set; }
  [Inject]
  private StudentContext _studentContext { get; set; }
  [Inject]
  private CategoryHubManager _categoryHubManager { get; set; }
  [Inject]
  private UserContext _userContext { get; set; }
  [Inject]
  private IOptionService _optionService { get; set; }
  private Category Category { get; set; } = new();
  private List<Field> Fields { get; set; } = new();


  protected override void OnInitialized() {
    _studentContext.OnChange += HandleStateChange;
    AssignNewCategory();
  }

  private void AddField() {
    Fields.Add(new Field {
      Id = Guid.NewGuid(),
      Name = string.Empty,
      IsRequired = false,
      CategoryId = Category.Id,
      FieldType = FieldType.Text,
      Options = new List<Option>()
    });
  }

  private void AddOption(int index) {
    Fields[index].Options.Add(new Option() {
      Name = string.Empty
    });
  }
  
  private void UpdateField(ChangeEventArgs e, int index) {
    Fields[index].Name = e.Value.ToString();
  }
  
  private void UpdateFieldOption(ChangeEventArgs e, int indexField, int indexOption) {
    Fields[indexField].Options.ElementAtOrDefault(indexOption).Name = e.Value.ToString();
  }
  
  private void AssignNewCategory() {
    Category = new() {
      Id = Guid.NewGuid(),
      Name = string.Empty,
      Description = string.Empty
    };
  
    Fields.Clear();
  }

  private async Task CreateNewCategory() {
    AssignNewCategory();
    ExitCategoryCreation();
  
    await _categoryService.Create(Category);
    
    foreach (var field in Fields) {
      if (field.Name != string.Empty) {
        if (field.Options != null) {
          if (!field.Options.Count.Equals(0)) {
            foreach (var option in field.Options) {
              await _optionService.Create(option);
            }
          }
        }
  
        await _fieldService.Create(field);
      }
    }
    
    await _categoryHubManager.SendCategoriesUpdatedAsync(_userContext.User.InstitutionId.ToString() ?? string.Empty);
  }
  
  private void ExitCategoryCreation() {
    _studentContext.SetCategoryCreation(false);
  }

  private void Update(ChangeEventArgs e, string update) {
    string value = e.Value?.ToString() ?? string.Empty;
  
    switch (update) {
      case "name":
        Category.Name = value;
        break;
      case "description":
        Category.Description = value;
        break;
    }
  }

  private void HandleStateChange() {
    StateHasChanged();
  }
}