namespace edu_institutional_management.Server.Models;

public class User {
  public Guid Id { get; set; }
  public string? Name { get; set; }
  public string? LastName { get; set; }
  public DateTime BirthDate { get; set; }
  public int Age { get; set; }
}