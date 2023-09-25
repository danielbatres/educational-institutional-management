using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class PaymentType {
  public int Id { get; set; }
  public string? Name { get; set; }
  public string? Description { get; set; }
  public float Amount { get; set; }
  [JsonIgnore]
  public ICollection<Payment>? Payments { get; set; }
} 