using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class StudentRegister : MainRegister {
  public DateTime CreatedAt { get; set; }
  public Guid StudentId { get; set; }
  [JsonIgnore]
  public Student? Student { get; set; }
}