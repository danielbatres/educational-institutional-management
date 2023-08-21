namespace edu_institutional_management.Shared.Models;

public class Payment {
  public Guid Id { get; set; }
  public float Amount { get; set; }
  public string PaymentType { get; set; }
}