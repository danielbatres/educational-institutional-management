using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Server.Services;

public class StatisticService : BaseService, IStatisticService {
	public StatisticService (ApplicationContextService applicationContextService) : base (applicationContextService){}
	
	public IEnumerable<Statistic> Get() {
		return _applicationContext.Statistics;
	}
	
	public async Task Create(Statistic statistic) {
		_applicationContext.Statistics.Add(statistic);
		
		await _applicationContext.SaveChangesAsync();
	}
	
	public async Task Update(Statistic statistic) {
		var originalStatistic = _applicationContext.Statistics.FirstOrDefault(s => s.Id.Equals(statistic.Id));
		
		if (originalStatistic != null) {
			_applicationContext.Entry(originalStatistic).State = EntityState.Detached;
			_applicationContext.Entry(originalStatistic).State = EntityState.Modified;
			
			await _applicationContext.SaveChangesAsync();
		}
	}
	
	public async Task Delete(Guid statisticId) {
		var originalStatistic = _applicationContext.Statistics.FirstOrDefault(s => s.Id.Equals(statisticId));
		
		if (originalStatistic != null) {
			_applicationContext.Statistics.Remove(originalStatistic);
			
			await _applicationContext.SaveChangesAsync();
		}
	}
}

public interface IStatisticService {
	IEnumerable<Statistic> Get();
	Task Create(Statistic statistic);
	Task Update(Statistic statistic);
	Task Delete(Guid statisticId);
}