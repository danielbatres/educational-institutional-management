using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class Rating {
  public int Id { get; set; }
  public float RatingValue { get; set; }
  public Guid StudentId { get; set; }
  public Guid RatingsListId { get; set; }
  [JsonIgnore]
  public RatingsList? RatingsList { get; set; }
}
