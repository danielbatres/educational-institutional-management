using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class Register : MainRegister {
  public string? AuthenticationMethod { get; set; }
  public Guid UserId { get; set; }
  [JsonIgnore]
  public User? User { get; set; }
}