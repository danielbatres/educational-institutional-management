using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class Option {
  public int Id { get; set; }
  public string Name { get; set; }
  public Guid FieldId { get; set; }
  [JsonIgnore]
  public Field? Field { get; set; }
}