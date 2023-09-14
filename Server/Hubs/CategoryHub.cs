using edu_institutional_management.Server.Services;
using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace edu_institutional_management.Server.Hubs;

public class CategoryHub : MainHub {
  private readonly ICategoryService _categoryService;
  
  public CategoryHub(ICategoryService categoryService) {
    _categoryService = categoryService;
  }
  
  public async Task SendCategoriesUpdate(string groupName) {
    var categories = GetCategories();
    await Clients.Group(groupName).SendAsync("CategoriesUpdated", categories);
  }
  
  public List<Category> GetCategories() {
    return _categoryService.Get().ToList();
  }
}