using edu_institutional_management.Shared.Models;
using edu_institutional_management.Server.Services;
using Microsoft.AspNetCore.SignalR;

namespace edu_institutional_management.Server.Hubs;

public class InstitutionHub : MainHub {
  private readonly IInstitutionService _institutionService;

  public InstitutionHub(IInstitutionService chatService) {
    _institutionService = chatService;
  }
  
  public async Task SendInstitutionUpdate(string groupName) {
    var institution = GetInstitution(Guid.Parse(groupName));
  
    await Clients.Group(groupName).SendAsync("InstitutionUpdated", institution);
  }
  
  public Institution GetInstitution(Guid institutionId) {
    return _institutionService.GetInstitution(institutionId);
  }
}