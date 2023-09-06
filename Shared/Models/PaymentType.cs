namespace edu_institutional_management.Shared.Models;

public class PaymentType {
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string Amount { get; set; }
  public virtual ICollection<Payment> Payments { get; set; }
} 