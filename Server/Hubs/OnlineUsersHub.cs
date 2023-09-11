using edu_institutional_management.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace edu_institutional_management.Server.Hubs;

public class OnlineUsersHub : Hub {
  private static List<User> Users { get; } = new();

  public async Task Connect(User user) {
    if (!Users.Contains(user)) {
      Users.Add(user);
      await Clients.All.SendAsync("UserConnected", user);
    }
  }

  public async Task Disconnect(User user) {
    Users.Remove(user);
    await Clients.All.SendAsync("UserDisconnected", user);
  }

  public List<User> GetInstitutionUsers(Guid id) {
    return Users.Where(u => u.InstitutionId == id).ToList();
  }

  public async Task DisconnectUser(Guid id) {
    var user = Users.FirstOrDefault(u => u.Id == id);

    if (user != null) {
      await Disconnect(user);
    }
  }
}