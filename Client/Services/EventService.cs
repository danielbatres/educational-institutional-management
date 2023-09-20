using System.Net.Http.Json;
using System.Text.Json;
using edu_institutional_management.Shared.Models;

namespace edu_institutional_management.Client.Services;

public class EventService : BaseService, IEventService {
	public EventService(HttpClient httpClient) : base(httpClient) {}
	
	public async Task<List<Event>> Get() {
		var response = await HttpClient.GetAsync("api/event");
		
		var content = await CheckResponseContent(response);
		
		return JsonSerializer.Deserialize<List<Event>>(content, JsonOptions) ?? new();
	}
	
	public async Task Create(Event eventModel) {
		var response = await HttpClient.PostAsJsonAsync("api/event/create", eventModel);
		
		await CheckResponse(response);
	}
	
	public async Task Update(Event eventModel) {
		var response = await HttpClient.PutAsJsonAsync("api/event/update", eventModel);
		
		await CheckResponse(response);
	}
}

public interface IEventService {
	Task Create(Event eventModel);
	Task Update(Event eventModel);
	Task<List<Event>> Get();
}