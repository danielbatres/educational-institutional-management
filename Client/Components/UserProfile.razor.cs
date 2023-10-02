using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace edu_institutional_management.Client.Components;

public partial class UserProfile {
  private string ImageSrc { get; set; } = string.Empty;
  private User UserBackup;
  private User NewUser;
  [Inject]
  public UserContext UserContext { get; set; }
  [Inject]
  public LoadingContext LoadingContext { get; set; }
  [Inject]
  public IUserService UserService { get; set; }
  private List<string> GradientColors { get; set; } = new() {
    "linear-gradient(90deg, hsla(39, 100%, 71%, 1) 0%, hsla(216, 100%, 62%, 1) 100%)", "linear-gradient(90deg, hsla(352, 83%, 64%, 1) 0%, hsla(230, 90%, 68%, 1) 100%)",
    "linear-gradient(90deg, hsla(46, 95%, 56%, 1) 0%, hsla(350, 97%, 65%, 1) 100%)", "linear-gradient(90deg, hsla(247, 23%, 40%, 1) 0%, hsla(42, 100%, 66%, 1) 100%)",
    "linear-gradient(90deg, hsla(120, 6%, 90%, 1) 0%, hsla(228, 75%, 16%, 1) 100%)", "linear-gradient(90deg, hsla(148, 100%, 47%, 1) 0%, hsla(211, 90%, 47%, 1) 100%)",
    "linear-gradient(90deg, hsla(148, 89%, 78%, 1) 0%, hsla(210, 81%, 22%, 1) 100%)", "linear-gradient(90deg, hsla(286, 48%, 91%, 1) 0%, hsla(340, 73%, 75%, 1) 50%, hsla(263, 58%, 45%, 1) 100%)"
  };
  private bool HasChanges = false; 
  private bool ShowGradients { get; set; } = false;

  protected override void OnInitialized() {
    UserContext.OnChange += HandleStateChange;

    if (UserContext.User != null) {
      UserBackup = UserContext.User.Clone();
      NewUser = UserContext.User.Clone();

      SetUserPhoto();
    }
  }

  private void HandleStateChange() {
    SetUserPhoto(); 
    StateHasChanged(); 
  }

  private void SetUserPhoto() {
    if (UserContext.User.Photo != null) {
      var imageBase64 = Convert.ToBase64String(UserContext.User.Photo);
      ImageSrc = $"data:image/png;base64,{imageBase64}";
    }
  }

  private async Task UpdateUser() {
    LoadingContext.SetLoading(true);
    LoadingContext.SetLoadingMessage("Actualizando tu usuario...");

    await UserService.Update(NewUser);
    UserBackup = UserContext.User.Clone();
    NewUser = UserContext.User.Clone();
    HasChanges = false;
    ShowGradients = false;
    await Task.Delay(500);
    LoadingContext.SetLoading(false);
  }

  private async Task OnInputFileChange(InputFileChangeEventArgs e) {
    var imageFile = await e.File.RequestImageFileAsync("image/png", 1920, 1920);
    var imageBytes = new byte[imageFile.Size];
    await imageFile.OpenReadStream().ReadAsync(imageBytes);

    UserContext.User.Photo = imageBytes;
    await UserService.Update(UserContext.User);
  }

  private void SetChanges() {
    HasChanges = !UserBackup.Equals(NewUser);
  }

  private void RevertChanges() {
    NewUser = UserBackup.Clone();

    HasChanges = false;
  }

  private void UpdateGradientColor(string color) {
    if (color != NewUser.PreferredBackgroundColor) {
      NewUser.PreferredBackgroundColor = color;
      SetChanges();
    }
  }

  private void UpdateUserName(ChangeEventArgs e) {
    NewUser.UserName = e.Value.ToString();
    SetChanges();
  }

  private void UpdateName(ChangeEventArgs e) {
    NewUser.Name = e.Value.ToString();
    SetChanges();
  }

  private void UpdateLastName(ChangeEventArgs e)
  {
    NewUser.LastName = e.Value.ToString();
    SetChanges();
  }

  private void UpdateLocation(ChangeEventArgs e) {
    NewUser.Location = e.Value.ToString();
    SetChanges();
  }

  private void UpdateBio(ChangeEventArgs e) {
    NewUser.Bio = e.Value.ToString();
    SetChanges();
  }

  private void UpdateEmail(ChangeEventArgs e) {
    NewUser.Register.Email = e.Value.ToString();
    SetChanges();
  }

  private void UpdatePhoneNumber(ChangeEventArgs e) {
    NewUser.PhoneNumber = e.Value.ToString();
    SetChanges();
  }
}