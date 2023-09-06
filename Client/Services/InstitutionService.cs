using System.Net.Http.Json;
using System.Text.Json;
using edu_institutional_management.Shared.Models;
using edu_institutional_management.Client.Containers;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace edu_institutional_management.Client.Services;

public class InstitutionService : BaseService, IInstitutionService {
  public InstitutionService(HttpClient httpClient) : base(httpClient) { }

  public async Task RegisterNewInstitution(Institution institution) {
    institution.DataBaseConnectionName = $"db_{institution.Id}";

    var response = await HttpClient.PostAsJsonAsync("api/institution/create", institution);

    await CheckResponse(response);

    Console.WriteLine(CheckResponse(response));
  }´k
}

public interface IInstitutionService {
  Task RegisterNewInstitution(Institution institution);
}