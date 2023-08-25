using System.ComponentModel.DataAnnotations;
using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;

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
  public User User { get; set; } = new();
  private string Password { get; set; } = string.Empty;
  private string Email { get; set; } = string.Empty;
  private string PasswordClass { get; set; } = string.Empty;
  private bool ShowPassword { get; set; } = false;
  private List<List<object>> Warnings { get; set; } = new List<List<object>>() { 
    new List<object> { "", false }, 
    new List<object> { "", false }, 
    new List<object> { "", false }, 
    new List<object> { "", false } 
  };
  private int ErrorsQuantity { get; set; } = 0;
  
  private void ValidatePassword() {
    Warnings[3][1] = false;

      switch (Validators.IsValidPassword(Password)) {
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
          ErrorsQuantity++;
          break;
    }
  }

  private void ValidateEmail() {
    Warnings[2][0] = string.Empty;
    Warnings[2][1] = false;

    if (!Validators.IsValidEmail(Email)) {
      Warnings[2][0] = "Correo electrónico invalido";
      Warnings[2][1] = true;
      ErrorsQuantity++;
    } else {
      ErrorsQuantity++;
    }
  }

  private void AssignUserData() {
    User.Id = Guid.NewGuid();
    User.Register = new Register {
      Id = Guid.NewGuid(),
      AuthenticationMethod = "EmailAndPassword",
      Email = Email,
      Password = Password,
      UserId = User.Id
    };

    User.OnlineStatus = new OnlineStatus {
      Id = Guid.NewGuid(),
      Status = true,
      LastConnection = DateTime.Now,
      UserId = User.Id
    };
  }

  private void CreateNewUser() {
    MakeValidations();

    if (ErrorsQuantity == 0) {
      AssignUserData();
      UserService.Register(User);

      NavBarContext.ChangeHideButtons(false);
      NavigationManager.NavigateTo("/");
    }

    ErrorsQuantity = 0;
  }

  private void UpdatePassword(ChangeEventArgs e) {
    Password = e.Value.ToString();
    ValidatePassword();
  }

  private void UpdateEmail(ChangeEventArgs e) {
    Email = e.Value.ToString();
    ValidateEmail();
  }

  private void ValidateText(string text) {
    Validators.MaxMinLength(text, 3, "min");
    Validators.MaxMinLength(text, 14, "max");
  }

  private void MakeValidations() {
    ValidateEmail();
    ValidatePassword();
  }
}
