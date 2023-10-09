using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class PaymentStudentRecord {
  public Guid Id { get; set; }
  public DateTime PaymentDate { get; set; }
  public decimal AmountPaid { get; set; }
  public Guid StudentId { get; set; }
  [JsonIgnore]
  public Student? Student { get; set; }
}