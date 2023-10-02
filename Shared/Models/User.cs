using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace edu_institutional_management.Shared.Models;

public class User : MainUser {
  public string UserName { get; set; }
  public string? Bio { get; set; }
  public string? Location { get; set; }
  public Guid? InstitutionId { get; set; }
  public Institution? Institution { get; set; }
  public OnlineStatus? OnlineStatus { get; set; }
  public Register? Register { get; set; }
  public Payment? Payment { get; set; }
  public string? PreferredBackgroundColor { get; set; }
  public ICollection<TelephoneNumber>? TelephoneNumbers { get; set; }
  public ICollection<MembershipRequest>? ReceivedMembershipRequests { get; set; }
  [NotMapped]
  [JsonIgnore]
  public List<UserRole>? UserRoles { get; set; }
  [NotMapped]
  [JsonIgnore]
  public Settings? Settings { get; set; }

  public User Clone() {
    return new User() {
      Id = Id, 
      Name = Name, 
      LastName = LastName, 
      Age = Age,
      Bio = Bio,
      PhoneNumber = PhoneNumber,
      BirthDate = BirthDate,
      Location = Location,
      PreferredBackgroundColor = PreferredBackgroundColor,
      Institution = Institution,
      InstitutionId = InstitutionId,
      OnlineStatus = OnlineStatus,
      Register = Register,
      Payment = Payment,
      ReceivedMembershipRequests = ReceivedMembershipRequests,
      Settings = Settings,
      UserName = UserName,
      UserRoles = UserRoles,
      Photo = Photo
    };
  }
}