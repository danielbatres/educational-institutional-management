namespace edu_institutional_management.Shared.DTO;

public class PaymentDto {
  public Guid Id { get; set; }
  public float Amount { get; set; }
  public string PaymentType { get; set; }
}