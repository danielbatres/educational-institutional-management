using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class FieldInformation {
  public int Id { get; set; }
  public string Information { get; set; }
  public Guid StudentId { get; set; }
  [JsonIgnore]
  public Student? Student { get; set; }
  public Guid FieldId { get; set; }
  public Field? Field { get; set; }
}