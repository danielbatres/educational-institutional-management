using Microsoft.AspNetCore.Components;
using edu_institutional_management.Client.Models;
using edu_institutional_management.Client.Containers;
using System.ComponentModel;

namespace edu_institutional_management.Client.Components;

public partial class Bar {
  [Inject]
  private NavigationManager? NavigationManager { get; set; }
  [Inject]
  private SideBarContext SideBarContext { get; set; }
  [Parameter]
  [EditorRequired]
  public bool IsToggle { get; set; }
  [CascadingParameter(Name = "Toggle")]
  public bool Toggle { get; set; }
  [Parameter]
  [EditorRequired]
  public Menu Menu { get; set; } = new Menu();
  private string SelectedContainer { get; set; } = "";
  [Parameter]
  public string Styles { get; set; } = string.Empty;
  [Parameter]
  [EditorRequired]
  public string SideBarOption { get; set; } = string.Empty;

  public void SetSelectedValues(string navigateTo, string selected) {
    switch (SideBarOption) {
      case "main-menu":
        SideBarContext.SetSelectedOptionMainMenu(int.Parse(selected));
        break;
      case "settings-menu":
        SideBarContext.SetSelectedOptionSettingsMenu(int.Parse(selected));
        break;
    }

    NavigationManager?.NavigateTo(navigateTo);
  }

  protected override void OnInitialized() {
    SideBarContext.OnChange += HandleStateChange;
  }

  private string GetSelectedContainer() {
    string container = string.Empty;

    switch (SideBarOption) {
      case "main-menu":
        container = SideBarContext.SelectedOptionMainMenu.ToString();
        break;
      case "settings-menu":
        container = SideBarContext.SelectedOptionSettingsMenu.ToString();
        break;
    }

    return container;
  }

  private void HandleStateChange() {
    StateHasChanged();
  }
}