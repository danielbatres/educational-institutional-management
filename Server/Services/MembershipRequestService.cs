using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class MembershipRequestService : IMembershipRequestService
{
  private readonly MainContext _context;

  public MembershipRequestService(MainContext dbContext) {
    _context = dbContext;
  }

  public async Task Create(MembershipRequest request) {
    _context.MembershipRequests.Add(request);

    await _context.SaveChangesAsync();
  }
}

public interface IMembershipRequestService {
  Task Create(MembershipRequest request);
}