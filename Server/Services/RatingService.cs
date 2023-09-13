using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class RatingService : BaseService, IRatingService {
  public RatingService(ApplicationContextService applicationContextService) : base(applicationContextService) {}

  public async Task Create(Rating rating) {
    _applicationContext.Ratings.Add(rating);
    
    await _applicationContext.SaveChangesAsync();
  }
  
  public async Task Update(Rating rating) {
    _applicationContext.Ratings.Update(rating);
    
    await _applicationContext.SaveChangesAsync();
  }
}

public interface IRatingService {
  Task Create(Rating rating);
  Task Update(Rating rating);
}