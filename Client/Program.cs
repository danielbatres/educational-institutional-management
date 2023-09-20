using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Client;
using Microsoft.AspNetCore.Components.Authorization;
using edu_institutional_management.Client.Hubs;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore(options =>
{
  options.AddPolicy("MyPolicy", policy =>
  {
    policy.RequireClaim("ClaimType");
  });
});

builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<LoadingSiteContext>();
builder.Services.AddSingleton<UserContext>();
builder.Services.AddSingleton<NavBarContext>();
builder.Services.AddSingleton<RegisterInstitutionContext>();
builder.Services.AddSingleton<LoginContext>();
builder.Services.AddSingleton<LoadingContext>();
builder.Services.AddSingleton<RoleContext>();
builder.Services.AddSingleton<ThemeContext>();
builder.Services.AddSingleton<StudentContext>();
builder.Services.AddSingleton<CourseContext>();
builder.Services.AddSingleton<Validators>();
builder.Services.AddSingleton<UsersHubManager>();
builder.Services.AddSingleton<RolesHubManager>();
builder.Services.AddSingleton<ActivityHubManager>();
builder.Services.AddSingleton<CategoryHubManager>();
builder.Services.AddSingleton<StudentHubManager>();
builder.Services.AddSingleton<ChatHubManager>();
builder.Services.AddSingleton<InstitutionHubManager>();
builder.Services.AddSingleton<StudentSettingsHubManager>();
builder.Services.AddSingleton<SubjectHubManager>();
builder.Services.AddSingleton<CourseHubManager>();
builder.Services.AddSingleton<HubsConnection>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IInstitutionService, InstitutionService>();
builder.Services.AddScoped<IMembershipRequestService, MembershipRequestService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ISettingsService, SettingsService>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFieldService, FieldService>();
builder.Services.AddScoped<IOptionService, OptionService>();
builder.Services.AddScoped<IStudentSettingsService, StudentSettingsService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IFieldInformationService, FieldInformationService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<ISubjectCourseService, SubjectCourseService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICourseScheduleService, CourseScheduleService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IAttendanceScheduleService, AttendanceScheduleService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

await builder.Build().RunAsync();
