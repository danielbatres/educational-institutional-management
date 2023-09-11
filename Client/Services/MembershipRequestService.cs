using System.Net.Http.Json;
using System.Text.Json;
using edu_institutional_management.Shared.Models;
namespace edu_institutional_management.Client.Services;

public class MembershipRequestService : BaseService, IMembershipRequestService
{
  public MembershipRequestService(HttpClient httpClient) : base(httpClient) { }

  public async Task Create(MembershipRequest request) {
    var response = await HttpClient.PostAsJsonAsync("api/membershiprequest/create", request);

    await CheckResponse(response);
  }
}

public interface IMembershipRequestService
{
  Task Create(MembershipRequest request);
}
