namespace edu_institutional_management.Server.Models;

public class Register {
  public Guid Id { get; set; }
  public string Email { get; set; }
  public string Password { get; set; }
  public string AuthenticationMethod { get; set; }
  public User User { get; set; }
  public Guid UserId { get; set; }
}