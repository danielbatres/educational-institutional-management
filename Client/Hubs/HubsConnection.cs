namespace edu_institutional_management.Client.Hubs;

public class HubsConnection {
  private readonly UsersHubManager _usersHubManager;
  private readonly RolesHubManager _rolesHubManager;
  private readonly CategoryHubManager _categoryHubManager;
  private readonly StudentHubManager _studentHubManager;
  private readonly ActivityHubManager _activityHubManager;
  private readonly ChatHubManager _chatHubManager;
  private readonly InstitutionHubManager _institutionHubManager;
  private readonly StudentSettingsHubManager _studentSettingsHubManager;
  
  public HubsConnection(StudentHubManager studentHubManager, ActivityHubManager activityHubManager, ChatHubManager chatHubManager, UsersHubManager usersHubManager, RolesHubManager rolesHubManager, CategoryHubManager categoryHubManager, InstitutionHubManager institutionHubManager, StudentSettingsHubManager studentSettingsHubManager) {
    _rolesHubManager = rolesHubManager;
    _categoryHubManager = categoryHubManager;
    _studentHubManager = studentHubManager;
    _activityHubManager = activityHubManager;
    _chatHubManager = chatHubManager;
    _usersHubManager = usersHubManager;
    _institutionHubManager = institutionHubManager;
    _studentSettingsHubManager = studentSettingsHubManager;
  }
  
  public async Task ConnectHubs(string groupName) {
    await _usersHubManager.StartConnectionAsync();
    await _rolesHubManager.StartConnectionAsync();
    await _categoryHubManager.StartConnectionAsync();
    await _studentHubManager.StartConnectionAsync();
    await _activityHubManager.StartConnectionAsync();
    await _chatHubManager.StartConnectionAsync();
    await _institutionHubManager.StartConnectionAsync();
    await _studentSettingsHubManager.StartConnectionAsync();

    await _usersHubManager.JoinGroup(groupName);
    await _rolesHubManager.JoinGroup(groupName);
    await _categoryHubManager.JoinGroup(groupName);
    await _studentHubManager.JoinGroup(groupName);
    await _activityHubManager.JoinGroup(groupName);
    await _chatHubManager.JoinGroup(groupName);
    await _institutionHubManager.JoinGroup(groupName);
    await _studentSettingsHubManager.JoinGroup(groupName);
  }
}