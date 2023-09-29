using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class RatingsListService : BaseService, IRatingsListService {
  public RatingsListService(ApplicationContextService applicationContextService) : base(applicationContextService) {}

  public IEnumerable<RatingsList> Get() {
    return _applicationContext.RatingsLists.Include(r => r.Ratings);
  }

  public async Task Create(RatingsList ratingsList) {
    _applicationContext.RatingsLists.Add(ratingsList);

    await _applicationContext.SaveChangesAsync();
  }

  public async Task Update(RatingsList ratingsList) {
    var originalRatingsList = _applicationContext.RatingsLists.FirstOrDefault(r => r.Id == ratingsList.Id);

    if (originalRatingsList != null) {
      _applicationContext.Entry(originalRatingsList).State = EntityState.Detached;
      _applicationContext.Entry(ratingsList).State = EntityState.Modified;

      await _applicationContext.SaveChangesAsync();
    }
  }

  public async Task Delete(Guid ratingsListId) {
    var originalRatingsList = _applicationContext.RatingsLists.FirstOrDefault(r => r.Id == ratingsListId);

    if (originalRatingsList != null) {
      _applicationContext.RatingsLists.Remove(originalRatingsList);

      await _applicationContext.SaveChangesAsync();
    }
  }
}

public interface IRatingsListService {
  IEnumerable<RatingsList> Get();
  Task Create(RatingsList ratingsList);
  Task Update(RatingsList ratingsList);
  Task Delete(Guid ratingsListId);
}