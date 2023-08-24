using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using edu_institutional_management.Client.Containers;
using edu_institutional_management.Client.Services;
using edu_institutional_management.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<UserContext>();
builder.Services.AddSingleton<NavBarContext>();
builder.Services.AddSingleton<RegisterInstitutionContext>();
builder.Services.AddSingleton<LoginContext>();
builder.Services.AddSingleton<LoadingContext>();
builder.Services.AddSingleton<Validators>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IUserService, UserService>();

await builder.Build().RunAsync();
