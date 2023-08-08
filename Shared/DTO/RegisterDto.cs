namespace edu_institutional_management.Shared.DTO;

public class RegisterDto
{
  public Guid Id { get; set; }
  public string Email { get; set; }
  public string Password { get; set; }
  public string AuthenticationMethod { get; set; }
  public UserDto User { get; set; }
  public Guid UserId { get; set; }
}