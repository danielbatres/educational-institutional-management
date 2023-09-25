namespace edu_institutional_management.Client.Services;

public class DatabaseService : BaseService, IDatabaseService {
  public DatabaseService(HttpClient httpClient) : base(httpClient) {}

  public async Task EnsureMainDb() {
    var response = await HttpClient.GetAsync("api/server/create-main-db");

    await CheckResponse(response);
  }
}

public interface IDatabaseService {
  Task EnsureMainDb();
}