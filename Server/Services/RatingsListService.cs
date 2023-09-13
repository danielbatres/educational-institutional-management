using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class RatingsListService {

}

public interface IRatingsListService {
  Task Create(RatingsList ratingsList);
  Task Update(RatingsList ratingsList);
}