namespace edu_institutional_management.Client.Containers;

public class ContentContext : BaseContainer {
  public string Section { get; set; } = string.Empty;
  public string Content { get; set; } = string.Empty;

  public void SetSectionContent(string section, string content) {
    Section = section;
    Content = $"{section} > {content}";

    NotifyStateChanged();
  }
}