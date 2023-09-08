using System.Net.Http.Json;
using System.Text.Json;
using edu_institutional_management.Shared.Models;
namespace edu_institutional_management.Client.Services;

public class InstitutionService : BaseService, IInstitutionService {
  public InstitutionService(HttpClient httpClient) : base(httpClient) { }

  public async Task RegisterNewInstitution(Institution institution) {
    institution.DataBaseConnectionName = $"DB_{institution.Name.Replace(" ", "")}_{institution.Id}";

    var response = await HttpClient.PostAsJsonAsync("api/institution/create", institution);

    await CheckResponse(response);

    DataBaseConnectionRequest request = new() {
      DataBaseName = institution.DataBaseConnectionName
    };

    await CreateInstitutionDataBase(request);
  }

  private async Task CreateInstitutionDataBase(DataBaseConnectionRequest dbName) {
    var response = await HttpClient.PostAsJsonAsync("/api/server/db-connection", dbName);

    await CheckResponse(response);

    await CheckResponse(await HttpClient.GetAsync("/api/server/create-application-db"));
  }

  public async Task<List<User>> GetInstitutionUsers(Guid institutioId) {
    var response = await HttpClient.GetAsync($"api/institution/get-institution-users?institutionId={institutioId}");

    var content = await CheckResponseContent(response);

    return JsonSerializer.Deserialize<List<User>>(content, JsonOptions) ?? new();
  }
}

public interface IInstitutionService {
  Task RegisterNewInstitution(Institution institution);
  Task<List<User>> GetInstitutionUsers(Guid institutionUser);
}
