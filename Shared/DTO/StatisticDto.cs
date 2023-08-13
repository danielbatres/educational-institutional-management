namespace edu_institutional_management.Shared.DTO;

public class StatisticDto {
  public Guid Id { get; set; }
  public string StatisticType { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public DateTime CreationDate { get; set; }
  public DateTime UpdatedDate { get; set; }
}