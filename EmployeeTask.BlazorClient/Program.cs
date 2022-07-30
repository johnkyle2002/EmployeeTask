using Blazored.LocalStorage;
using EmployeeTask.BlazorClient;
using EmployeeTask.BlazorClient.FactoryService;
using EmployeeTask.Interface.Services;
using EmployeeTask.Shared.Configuration;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var configuration = builder.Configuration;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.Configure<HttpOptions>(option => configuration.GetSection("HttpApi").Bind(option));
builder.Services.AddScoped<AuthenticationStateProvider, CustomeAuthStateProvider>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

await builder.Build().RunAsync();
