namespace edu_institutional_management.Client.Models;

public class Menu {
  public List<string> MenuLabels { get; set; }
  public List<List<List<string>>> MenuItems { get; set; }
  public List<string> LastOption { get; set; }

  public Menu() {
    MenuLabels = new List<string>();
    MenuItems = new List<List<List<string>>>();
    LastOption = new List<string>();
  }
}