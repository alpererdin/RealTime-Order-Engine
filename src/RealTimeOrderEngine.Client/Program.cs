using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RealTimeOrderEngine.Client;
using RealTimeOrderEngine.Client.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using RealTimeOrderEngine.Client.Auth;
using RealTimeOrderEngine.Client.Handlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5211";

builder.Services.AddTransient<UnauthorizedResponseHandler>();

builder.Services.AddHttpClient("DefaultClient", client => 
{
    client.BaseAddress = new Uri(apiBaseUrl);
}).AddHttpMessageHandler<UnauthorizedResponseHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("DefaultClient"));

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddScoped<ProductApiService>();
builder.Services.AddScoped<OrderApiService>();
builder.Services.AddScoped<TableApiService>();
builder.Services.AddScoped<ReviewApiService>();
builder.Services.AddScoped<AuthApiService>();
builder.Services.AddScoped<StaffApiService>();

await builder.Build().RunAsync();