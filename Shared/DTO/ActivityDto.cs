namespace edu_institutional_management.Shared.DTO;

public class ActivityDto {
  public Guid Id { get; set; }
  public string Title { get; set; }
  public string Message { get; set; }
  public UserDto Author { get; set; }
  public DateTime Date { get; set; }
}