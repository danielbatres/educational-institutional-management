using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class MembershipRequest {
  public Guid Id { get; set; }
  public string Author { get; set; }
  public string InstitutionName { get; set; }
  public bool IsAccepted { get; set; }
  public DateTime CreationDate { get; set; }
  public string? Message { get; set; }
  public Guid ReceiverUserId { get; set; }
  [JsonIgnore]
  public virtual User? ReceiverUser { get; set; }
}