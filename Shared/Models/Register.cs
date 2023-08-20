using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class Register
{
  public Guid Id { get; set; }
  public string? Email { get; set; }
  public string? Password { get; set; }
  public string? AuthenticationMethod { get; set; }
  public Guid UserId { get; set; }
  [JsonIgnore]
  public virtual User? User { get; set; }
}