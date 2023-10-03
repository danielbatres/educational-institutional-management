using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace edu_institutional_management.Client.Shared.AccessComponents;

public partial class Login {
  [Inject]
  private NavigationManager NavigationManager { get; set; }
  [Inject]
  private IUserService UserService { get; set; }
  [Inject]
  private LoadingContext LoadingContext { get; set; }
  [Inject]
  private LoginContext LoginContext { get; set; }
  [Inject]
  private UserContext UserContext { get; set; }
  [Inject]
  private IJSRuntime JSRuntime { get; set; }
  private bool RememberMe { get; set; } = false;
  private List<List<object>> LoginInfo { get; set; } = new() {
    new List<object> { "", true },
    new List<object> { "", true }
  };
  private User User { get; set; } = new()
  {
    Register = new()
    {
      Email = string.Empty,
      Password = string.Empty
    }
  };
  private bool ShowPassword { get; set; } = false;

  private async Task HandleKeyPress(KeyboardEventArgs e) {
    if (e.Key == "Enter") await AuthUser();
  }

  protected override void OnInitialized() {
    UserContext.OnChange += HandleStateChanged;
  }

  private void HandleStateChanged() {
    StateHasChanged();
  }

  private async Task AuthUser()
  {
    LoadingContext.SetLoading(true);
    LoadingContext.SetLoadingMessage("Buscando tu usuario...");

    LoginInfo = await UserService.LoginUser(User, RememberMe);

    LoadingContext.SetLoading(false);

    if (!(bool)LoginInfo[0][1] || !(bool)LoginInfo[1][1]) {
      LoginContext.SetState(false);
      LoginContext.SetShowLoginStateModal(true, "No se encontro el usuario", "Fallo al inciar sesión");
    }
    else {
      LoginContext.SetState(true);
      LoginContext.SetShowLoginStateModal(true, "Usuario autenticado correctamente", "¡Inicio de sesión exitoso!");

      if (UserContext.User.InstitutionId != null) {
        await JSRuntime.InvokeVoidAsync("location.reload");
      } else {
        NavigationManager.NavigateTo("/");
      }
    }
  }
}