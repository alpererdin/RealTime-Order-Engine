using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RealTimeOrderEngine.Client;
using RealTimeOrderEngine.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5211";
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });

builder.Services.AddScoped<ProductApiService>();
builder.Services.AddScoped<OrderApiService>();
builder.Services.AddScoped<TableApiService>();
builder.Services.AddScoped<ReviewApiService>();

await builder.Build().RunAsync();