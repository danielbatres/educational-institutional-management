using System.Net.Http.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class CategoryService : BaseService, ICategoryService {
  public CategoryService(HttpClient httpClient) : base(httpClient) { }
  
  public async Task Create(Category category) {
    var response = await HttpClient.PostAsJsonAsync("api/category/create", category);
    
    await CheckResponse(response);
  }
  
  public async Task Update(Category category) {
    var response = await HttpClient.PutAsJsonAsync("api/category/update", category);
    
    await CheckResponse(response);
  }
}

public interface ICategoryService {
  Task Create(Category category);
}