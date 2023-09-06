using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class User {
  public Guid Id { get; set; }
  public string UserName { get; set; }
  public string? Name { get; set; }
  public string? LastName { get; set; }
  public DateTime? BirthDate { get; set; }
  public int Age { get; set; }
  public string? PhoneNumber { get; set; }
  public string? Bio { get; set; }
  public string? Location { get; set; }
  public Guid? InstitutionId { get; set; }
  public virtual Institution? Institution { get; set; }
  public virtual OnlineStatus OnlineStatus { get; set; }
  public virtual Register Register { get; set; }
  public virtual Payment? Payment { get; set; }

  public User Clone() {
    return new User() {
      Id = Id, 
      Name = Name, 
      LastName = LastName, 
      Age = Age,
      Bio = Bio,
      PhoneNumber = PhoneNumber,
      BirthDate = BirthDate,
      Location = Location,
      Institution = Institution,
      InstitutionId = InstitutionId,
      OnlineStatus = OnlineStatus,
      Register = Register
    };
  }
}