using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class RatingService : BaseService, IRatingService {
  public RatingService(ApplicationContextService applicationContextService) : base(applicationContextService) {}

  public async Task Create(Rating rating) {
    _applicationContext.Ratings.Add(rating);
    
    await _applicationContext.SaveChangesAsync();
  }
  
  public async Task Update(Rating rating) {
    var originalRating = _applicationContext.Ratings.FirstOrDefault(r => r.Id.Equals(rating.Id));

    if (originalRating != null) {
      _applicationContext.Entry(originalRating).State = EntityState.Detached;
      _applicationContext.Entry(rating).State = EntityState.Modified;

      await _applicationContext.SaveChangesAsync();
    }
  }
}

public interface IRatingService {
  Task Create(Rating rating);
  Task Update(Rating rating);
}