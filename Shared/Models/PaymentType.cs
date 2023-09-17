using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class PaymentType {
  public Guid Id { get; set; }
  public int IndexNumber { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public float Amount { get; set; }
  [JsonIgnore]
  public ICollection<Payment> Payments { get; set; }
} 