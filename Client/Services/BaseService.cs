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
}