using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class CategoryService : BaseService, ICategoryService {
  public CategoryService(ApplicationContextService applicationContextService) : base(applicationContextService) {}
  
  public IEnumerable<Category> Get() {
    return _applicationContext.Categories.Include(f => f.Fields);
  }
  
   public async Task Create(Category category) {
     _applicationContext.Categories.Add(category);
     
     await _applicationContext.SaveChangesAsync();
   }
   
   public async Task Update(Category category) {
     _applicationContext.Categories.Update(category);
     
     await _applicationContext.SaveChangesAsync();
   }
}

public interface ICategoryService {
  IEnumerable<Category> Get();
  Task Create(Category category);
  Task Update(Category category);
}