using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class TelephoneNumber {
  public int Id { get; set; }
  public string? PhoneNumber { get; set; }
  public Guid UserId { get; set; }
  [JsonIgnore]
  public User? User { get; set; }
}