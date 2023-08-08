namespace edu_institutional_management.Shared.DTO;

public class OnlineStatusDto
{
  public Guid Id { get; set; }
  public bool Status { get; set; }
  public DateTime LastConnection { get; set; }
  public UserDto User { get; set; }
  public Guid UserId { get; set; }
}