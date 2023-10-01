using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class Institution {
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Address { get; set; }
  public string PhoneNumber { get; set; }
  public string Email { get; set; }
  public string WebSite { get; set; }
  public string Country { get; set; }
  public string DataBaseConnectionName { get; set; }
  public bool IsActive { get; set; }
  public byte[]? Photo { get; set; }
  public DateTime RegisteredDate { get; set; }
  [JsonIgnore]
  public ICollection<User>? Users { get; set; }

  public Institution Clone() {
    return new() {
      Id = Id,
      Name = Name,
      Address = Address,
      PhoneNumber = PhoneNumber,
      Email = Email,
      WebSite = WebSite,
      Country = Country,
      DataBaseConnectionName = DataBaseConnectionName,
      IsActive = IsActive,
      Photo = Photo,
      RegisteredDate = RegisteredDate
    };
  }
}