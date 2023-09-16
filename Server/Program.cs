using edu_institutional_management.Server;
using edu_institutional_management.Server.Hubs;
using edu_institutional_management.Server.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IInstitutionService, InstitutionService>();
builder.Services.AddScoped<IMembershipRequestService, MembershipRequestService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ISettingsService, SettingsService>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IAttendanceScheduleService, AttendanceScheduleService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFieldService, FieldService>();
builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IFieldInformationService, FieldInformationService>();
builder.Services.AddSqlServer<MainContext>(builder.Configuration.GetConnectionString("cnMain"));
builder.Services.AddSingleton<ApplicationContextService>();

builder.Services.AddAuthentication(options => {
  options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
}).AddCookie().AddGoogle(googleOptions => {
  googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? string.Empty;
  googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? string.Empty;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseWebAssemblyDebugging();
}
else
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<UsersHub>("/usersHub");
app.MapHub<ChatHub>("/chatHub");
app.MapHub<RolesHub>("/rolesHub");
app.MapHub<NotificationHub>("/notificationHub");
app.MapHub<ActivityHub>("/activityHub");
app.MapHub<CategoryHub>("/categoryHub");
app.MapHub<StudentHub>("/studentsHub");
app.MapHub<InstitutionHub>("/institutionHub");

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
