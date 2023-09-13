namespace edu_institutional_management.Shared.Models;

public class PaymentRecord {
  public Guid Id { get; set; }
  public DateTime PaymentDate { get; set; }
  public decimal AmountPaid { get; set; }
  public Guid StudentId { get; set; }
  public Student? Student { get; set; }
  public Guid PaymentSettingsId { get; set; }
  public PaymentSettings? PaymentSettings { get; set; }
}