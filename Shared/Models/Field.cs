using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class Field {
  public Guid Id { get; set; }
  public string Name { get; set; }
  public bool IsRequired { get; set; }
  public Guid CategoryId { get; set; }
  [JsonIgnore]
  public Category? Category { get; set; }
  [JsonIgnore]
  public ICollection<FieldInformation>? FieldsInformation { get; set; }
}