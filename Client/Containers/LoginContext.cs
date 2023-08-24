namespace edu_institutional_management.Client.Containers; 

public class LoginContext : BaseContainer {
    public bool ShowLoginStateModal { get; set;} =  false;
    public string LoginStateMessage { get; set; }
    public string LoginStateTitle { get; set; }
    public bool IsSuccess { get; set; } = false;

    public void SetShowLoginStateModal(bool state, string message, string title) {
        ShowLoginStateModal = state;
        LoginStateMessage = message;
        LoginStateTitle = title;

        NotifyStateChanged();
    }

    public void ShutLoginState() {
        ShowLoginStateModal = false;

        NotifyStateChanged();
    }

    public void SetState(bool state) {
        IsSuccess = state;

        NotifyStateChanged();
    }
}