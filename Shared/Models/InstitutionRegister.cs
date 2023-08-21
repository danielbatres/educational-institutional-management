namespace edu_institutional_management.Shared.Models;

public class InstitutionRegister {
    public Guid Id { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime EndDate { get; set; }
    public User User { get; set; }
    public Guid InstitutionId { get; set; }
    public Institution Institution { get; set; }
}