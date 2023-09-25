using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace edu_institutional_management.Client.Components;

public partial class UserProfile {
  private User UserBackup;
  private User NewUser;
  [Inject]
  public UserContext UserContext { get; set; }
  [Inject]
  public LoadingContext LoadingContext { get; set; }
  [Inject]
  public IUserService UserService { get; set; }
  private bool HasChanges = false;

  protected override void OnInitialized() {
    UserContext.OnChange += HandleStateChange;

    if (UserContext.User != null) {
      UserBackup = UserContext.User.Clone();
      NewUser = UserContext.User.Clone();
    }
  }

  private void HandleStateChange() {
    StateHasChanged();
  }

  private async Task UpdateUser() {
    LoadingContext.SetLoading(true);
    LoadingContext.SetLoadingMessage("Actualizando tu usuario...");

    await UserService.Update(NewUser);
    UserBackup = UserContext.User.Clone();
    NewUser = UserContext.User.Clone();
    HasChanges = false;
    LoadingContext.SetLoading(false);
  }

  private void SetChanges() {
    HasChanges = !UserBackup.Equals(NewUser);
  }

  private void RevertChanges() {
    NewUser = UserBackup.Clone();

    HasChanges = false;
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