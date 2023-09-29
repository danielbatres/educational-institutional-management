using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class PaymentSettings {
  public Guid Id { get; set; }
  public string? PaymentName { get; set; }
  public string? PaymentDescription { get; set; }
  public decimal Amount { get; set; }
  public DateTime DueDate { get; set; }
  public bool IsRecurring { get; set; }
  public int FrequencyMonths { get; set; }
  [JsonIgnore]
  public ICollection<PaymentRecord>? PaymentRecords { get; set; }
}