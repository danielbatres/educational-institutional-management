using edu_institutional_management.Client.Services;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Client.Containers;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Components;

public partial class CategoryAction {
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
  [Inject]
  private Validators Validators { get; set; }
  private Category Category { get; set; } = new();
  private List<Field> Fields { get; set; } = new();
  [Parameter]
  public string ActionOption { get; set; } = string.Empty;
  private List<List<object>> Warnings { get; set; } = new() {
    new() { "", false },
    new() { "", false }
  };

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
      Id = Guid.NewGuid(),
      Name = string.Empty,
      FieldId = Fields[index].Id
    });
  }
  
  private void RemoveOption(int index, Option option) {
    Fields[index].Options.Remove(option);
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
    ExitCategoryCreation();

    await _categoryService.Create(Category);

    foreach (var field in Fields) {
      if (field.Name == string.Empty) continue;

      await _fieldService.Create(field);

      if (field.Options != null) {
        if (!field.Options.Count.Equals(0)) {
          foreach (var option in field.Options) {
            if (option.Name.Equals(string.Empty)) continue;
            await _optionService.Create(option);
          }
        }
      }
    }
    
    await _categoryHubManager.SendCategoriesUpdatedAsync(_userContext.User.InstitutionId.ToString() ?? string.Empty);

    AssignNewCategory();
  }
  
  private void ExitCategoryCreation() {
    _studentContext.SetCategoryCreation(false);
  }

  private void Update(ChangeEventArgs e, string update) {
    string value = e.Value.ToString();
  
    switch (update) {
      case "name":
        Category.Name = value;
        break;
      case "description":
        Category.Description = value;
        break;
    }
  }

  private void UpdateField(ChangeEventArgs e, int index) {
    Fields[index].Name = e.Value.ToString();
  }

  private void UpdateOption(ChangeEventArgs e, int index, int indexOption) {
    Fields[index].Options.ElementAtOrDefault(indexOption).Name = e.Value.ToString();
  }

  private void HandleStateChange() {
    StateHasChanged();
  }
}