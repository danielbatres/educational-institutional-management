using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server.Services;

public class EventService : BaseService, IEventService {
	public EventService(ApplicationContextService applicationContextService) : base (applicationContextService) {}
	
	public IEnumerable<Event> Get() {
		return _applicationContext.Events;
	}
	
	public async Task Create(Event eventModel) {
		_applicationContext.Events.Add(eventModel);
		
		await _applicationContext.SaveChangesAsync();
	}
	
	public async Task Update(Event eventModel) {
		var originalEvent = _applicationContext.Events.FirstOrDefault(e => e.Id.Equals(eventModel.Id));
		
		if (originalEvent != null) {
			_applicationContext.Entry(originalEvent).State = EntityState.Detached;
			_applicationContext.Entry(eventModel).State = EntityState.Modified;
			
			await _applicationContext.SaveChangesAsync();
		}
	}
	
	public async Task Delete(Guid eventId) {
		var originalEvent = _applicationContext.Events.FirstOrDefault(e => e.Id.Equals(eventId));

		if (originalEvent != null) {
			_applicationContext.Events.Remove(originalEvent);
			
			await _applicationContext.SaveChangesAsync();
		}
	}
}

public interface IEventService {
	IEnumerable<Event> Get();
	Task Create(Event eventModel);
	Task Update(Event eventModel);
	Task Delete(Guid eventId);
}