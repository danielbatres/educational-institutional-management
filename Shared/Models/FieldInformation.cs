namespace edu_institutional_management.Shared.Models;

public class FieldInformation {
  public int Id { get; set; }
  public string Information { get; set; }
  public Guid StudentId { get; set; }
  public Student Student { get; set; }
  public Field Field { get; set; }
}