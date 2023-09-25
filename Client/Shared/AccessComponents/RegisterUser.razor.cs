using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace edu_institutional_management.Client.Shared.AccessComponents;

public partial class RegisterUser {
  [Inject]
  private NavigationManager NavigationManager { get; set; }
  [Inject]
  private NavBarContext NavBarContext { get; set; }
  [Inject]
  private IUserService UserService { get; set; }
  [Inject]
  private Validators Validators { get; set; }
  [Inject]
  private LoadingContext LoadingContext { get; set; }
  public User User { get; set; } = new() {
    Name = string.Empty,
    LastName = string.Empty,
    UserName = string.Empty,
    Register = new() {
      Password = string.Empty,
      Email = string.Empty
    }
  };
  private string PasswordClass { get; set; } = string.Empty;
  private bool ShowPassword { get; set; } = false;
  private List<List<object>> Warnings { get; set; } = new List<List<object>>() { 
    new() { "", false }, 
    new() { "", false }, 
    new() { "", false }, 
    new() { "", false },
    new() { "", false },
  };
  private int ErrorsQuantity { get; set; } = 0;
  private string CharactersMessage { get; set; } = "Caracteres 2 - 40";
  private bool IsAccepted { get; set; } = false;

  private void GoogleSignIn() {
    NavigationManager.NavigateTo("/api/user/google-sign-in", true);
  }
  
  private void ValidatePassword() {
    Warnings[3][1] = false;

    if (User.Register != null && User.Register.Password != null)

    switch (Validators.IsValidPassword(User.Register.Password)) {
      case PasswordStrength.Weak:
        Warnings[3][0] = "Contraseña debil";
        Warnings[3][1] = true;
        PasswordClass = "danger-text";
        ErrorsQuantity++;
        break;
      case PasswordStrength.Moderate:
        Warnings[3][0] = "Contraseña moderada";
        Warnings[3][1] = true;
        PasswordClass = "warning-text";
        ErrorsQuantity++;
        break;
      case PasswordStrength.Strong:
        Warnings[3][0] = "Contraseña fuerte";
        PasswordClass = "success-text";
        break;
      default:
        Warnings[3][0] = string.Empty;
        Warnings[3][1] = true;
        ErrorsQuantity++;
        break;
    }
  }

  private async Task ValidateUserName() {
    Warnings[4][0] = string.Empty;
    Warnings[4][1] = false;

    if (await UserService.UserNameExists(User.UserName) || string.IsNullOrEmpty(User.UserName)) {
      Warnings[4][0] = "El username ya esta en uso";
      Warnings[4][1] = true;
      ErrorsQuantity++;
    }
  }

  private async Task ValidateEmail() {
    Warnings[2][0] = string.Empty;
    Warnings[2][1] = false;

    if (User.Register != null && User.Register.Email != null) {
      if (!Validators.IsValidEmail(User.Register.Email)) {
        Warnings[2][0] = "Correo electrónico invalido";
        Warnings[2][1] = true;
        ErrorsQuantity++;
      }

      if (await UserService.UserEmailExists(User.Register.Email)) {
        Warnings[2][0] = "El correo electrónico ya esta registrado";
        Warnings[2][1] = true;
        ErrorsQuantity++;
      }
    }
  }

  private void ValidateName() {
    SetCharacters(ValidateText(User.Name), 0);
  }

  private void ValidateLastName() {
    SetCharacters(ValidateText(User.LastName), 1);
  }

  private void SetCharacters(bool set, int index) {
    Warnings[index][0] = string.Empty;
    Warnings[index][1] = false;

    if (set) {
      Warnings[index][0] = CharactersMessage;
      Warnings[index][1] = true;
      ErrorsQuantity++;
    }
  }

  private void AssignUserData() {
    User.Id = Guid.NewGuid();
    User.Register.Id = Guid.NewGuid();
    User.Register.AuthenticationMethod = "EmailAndPassword";
    User.Register.UserId = User.Id;

    User.OnlineStatus = new OnlineStatus {
      Id = Guid.NewGuid(),
      Status = true,
      LastConnection = DateTime.Now,
      UserId = User.Id
    };
  }

  private async Task CreateNewUser() {
    if (!IsAccepted) return;
    
    LoadingContext.SetLoading(true);
    LoadingContext.SetLoadingMessage("Registrando tu usuario...");

    await MakeValidations();

    if (ErrorsQuantity == 0) {
      AssignUserData();

      await UserService.Register(User.Clone());
      NavBarContext.ChangeHideButtons(false);
      NavigationManager.NavigateTo("/");
    }

    LoadingContext.SetLoading(false);
  }

  private void UpdateName(ChangeEventArgs e) {
    User.Name = e.Value.ToString();
    ValidateName();
  }

  private void UpdateLastName(ChangeEventArgs e) {
    User.LastName = e.Value.ToString();
    ValidateLastName();
  }

  private void UpdateUserName(ChangeEventArgs e) {
    User.UserName = e.Value.ToString();
  }

  private void UpdatePassword(ChangeEventArgs e) {
    User.Register.Password = e.Value.ToString();
    ValidatePassword();
  }

  private void UpdateEmail(ChangeEventArgs e) {
    User.Register.Email = e.Value.ToString();
    ValidateEmail();
  }

  private bool ValidateText(string text) {
    return Validators.MaxMinLength(text, 2, "min") || Validators.MaxMinLength(text, 40, "max");
  }

  private async Task HandleKeyPress(KeyboardEventArgs e) {
    if (e.Key == "Enter") await CreateNewUser();
  }

  private async Task MakeValidations() {
    ErrorsQuantity = 0;

    ValidateName();
    ValidateLastName();
    await ValidateEmail();
    await ValidateUserName();
    ValidatePassword();
  }
}
