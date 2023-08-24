namespace edu_institutional_management.Client.Containers; 

public class LoginContext : BaseContainer {
    public bool ShowLoadingModal { get; set; } = false;
    public bool ShowLoginStateModal { get; set;} =  false;

    public void SetShowLoginStateModal(bool state) {
        ShowLoginStateModal = state;

        NotifyStateChanged();
    }
}