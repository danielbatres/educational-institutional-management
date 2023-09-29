using System.Net.Http.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class RatingService : BaseService, IRatingService {
  public RatingService(HttpClient httpClient) : base(httpClient) {}

  public async Task Create(Rating rating) {
    var response = await HttpClient.PostAsJsonAsync("api/ratings/create", rating);

    await CheckResponse(response);
  }

  public async Task Update(Rating rating) {
    var response = await HttpClient.PutAsJsonAsync("api/ratings/update", rating);

    await CheckResponse(response);
  }
}

public interface IRatingService {
  Task Create(Rating rating);
  Task Update(Rating rating);
}