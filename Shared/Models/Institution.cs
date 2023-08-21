using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class Institution {
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Address { get; set; }
  public string PhoneNumber { get; set; }
  public string Email { get; set; }
  public string WebSite { get; set; }
  public InstitutionRegister InstitutionRegister { get; set; }
  public virtual ICollection<User>? Users { get; set; }
}