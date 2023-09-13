namespace edu_institutional_management.Shared.Models;

public class PaymentSettings {
  public Guid Id { get; set; }
  public decimal Amount { get; set; }
  public DateTime DueDate { get; set; }
  public bool IsRecurring { get; set; }
  public int FrequencyMonths { get; set; }
}