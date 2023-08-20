using System.Text.Json;

namespace edu_institutional_management.Client.Services;

public class BaseService {
  protected readonly HttpClient HttpClient;
  protected readonly JsonSerializerOptions JsonOptions;

  public BaseService(HttpClient httpClient) {
    HttpClient = httpClient;
    JsonOptions = new JsonSerializerOptions {
      PropertyNameCaseInsensitive = true
    };
  }

  public async Task CheckResponse(HttpResponseMessage response) {
    var content = await response.Content.ReadAsStringAsync();

    if (!response.IsSuccessStatusCode) {
      throw new ApplicationException(content);
    }
  }

  public async Task<string> CheckResponseContent(HttpResponseMessage response) {
    var content = await response.Content.ReadAsStringAsync();

    if (!response.IsSuccessStatusCode) {
      throw new ApplicationException(content);
    }

    return content;
  }
}