namespace edu_institutional_management.Shared.Models;

public class Payment {
  public Guid Id { get; set; }
  public DateTime RegisterDate { get; set; }
  public DateTime EndDate { get; set; }
  public Guid PaymentTypeId { get; set; }
  public PaymentType PaymentType { get; set; }
  public virtual Guid UserId { get; set; }
  public virtual User User { get; set; }
}