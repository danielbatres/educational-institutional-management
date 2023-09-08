
using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class OnlineStatus
{
  public Guid Id { get; set; }
  public bool Status { get; set; }
  public DateTime LastConnection { get; set; }
  public Guid UserId { get; set; }
  [JsonIgnore]
  public virtual User? User { get; set; }
}