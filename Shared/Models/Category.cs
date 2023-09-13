namespace edu_institutional_management.Shared.Models;

public class Category {
  public Guid Id { get; set; }
  public string Name { get; set; }
  public ICollection<Field>? Fields { get; set; }
}