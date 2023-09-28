namespace edu_institutional_management.Client.Containers;

public class GeneralSearchContext : BaseContainer {
    public bool ShowGeneralSearch { get; set; } 
    public string SearchValue { get; set; } = string.Empty;

    public void SetSearchValue(string value) {
        SearchValue = value;
        ShowGeneralSearch = true;
    
        NotifyStateChanged();
    }

    public void SetShowGeneralSearch(bool show) {
        ShowGeneralSearch = show;

        NotifyStateChanged();
    }
}