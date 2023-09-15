using System.Net.Http.Json;
using System.Text.Json;
using edu_institutional_management.Client.Hubs;
using edu_institutional_management.Shared.Models;
namespace edu_institutional_management.Client.Services;

public class InstitutionService : BaseService, IInstitutionService {
  private readonly InstitutionHubManager _institutionHubManager;

  public InstitutionService(HttpClient httpClient, InstitutionHubManager institutionHubManager) : base(httpClient) {
    _institutionHubManager = institutionHubManager;
  }

  public async Task RegisterNewInstitution(Institution institution) {
    institution.DataBaseConnectionName = $"DB_{institution.Id}";

    var response = await HttpClient.PostAsJsonAsync("api/institution/create", institution);

    await CheckResponse(response);

    await CreateInstitutionDataBase(institution.DataBaseConnectionName);
  }

  public async Task Update(Institution institution) {
    var response = await HttpClient.PutAsJsonAsync("api/institution/update", institution);

    await CheckResponse(response);

    await _institutionHubManager.SendInstitutionUpdatedAsync(institution.Id.ToString());
  }

  public async Task<Guid> GetInstitutionId(string name) {
    var response = await HttpClient.GetAsync($"api/institution/get-institution-by?name={name}");

    var content = await CheckResponseContent(response);

    List<Institution> Institutions = JsonSerializer.Deserialize<List<Institution>>(content, JsonOptions) ?? new();

    if (Institutions.Count == 1) return Institutions[0].Id;

    return Guid.Empty;
  }

  private async Task CreateInstitutionDataBase(string dbName) {
    await SendInstitutionConnection(dbName);

    await CheckResponse(await HttpClient.GetAsync("/api/server/create-application-db"));
  }

  public async Task<List<User>> GetInstitutionUsers(Guid institutionId) {
    var response = await HttpClient.GetAsync($"api/institution/get-institution-users?institutionId={institutionId}");

    var content = await CheckResponseContent(response);

    return JsonSerializer.Deserialize<List<User>>(content, JsonOptions) ?? new();
  }

  public async Task SendInstitutionConnection(string dbName) {
    var response = await HttpClient.GetAsync($"/api/server/db-connection?dbName={dbName}");

    await CheckResponse(response);
  }
}

public interface IInstitutionService {
  Task<Guid> GetInstitutionId(string name);
  Task Update(Institution institution);
  Task RegisterNewInstitution(Institution institution);
  Task<List<User>> GetInstitutionUsers(Guid institutionUser);
  Task SendInstitutionConnection(string dbName);
}
