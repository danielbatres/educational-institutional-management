using Microsoft.AspNetCore.Components;
using edu_institutional_management.Client.Models;

namespace edu_institutional_management.Client.Components;

public partial class Bar {
  [Inject]
  private NavigationManager? NavigationManager { get; set; }
  [Parameter]
  [EditorRequired]
  public bool IsToggle { get; set; }
  [CascadingParameter(Name = "Toggle")]
  public bool Toggle { get; set; }
  [Parameter]
  [EditorRequired]
  public Menu Menu { get; set; } = new Menu();
  private string selectedContainer = "";
  [Parameter]
  public string Styles { get; set; } = string.Empty;

  public void SetSelectedValues(string navigateTo, string selected) {
    selectedContainer = selected;
    NavigationManager?.NavigateTo(navigateTo);
  }
}