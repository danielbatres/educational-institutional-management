using System.Net.Http.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class RatingsListService : BaseService, IRatingsListService {
  public RatingsListService(HttpClient httpClient) : base(httpClient) {}

  public async Task Create(RatingsList ratingsList) {
    var response = await HttpClient.PostAsJsonAsync("api/ratingslist/create", ratingsList);

    await CheckResponse(response);
  }

  public async Task Update(RatingsList ratingsList) {
    var response = await HttpClient.PutAsJsonAsync("api/ratingslist/update", ratingsList);

    await CheckResponse(response);
  }
}

public interface IRatingsListService {
  Task Create(RatingsList ratingsList);
  Task Update(RatingsList ratingsList);
}