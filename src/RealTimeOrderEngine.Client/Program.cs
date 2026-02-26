using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RealTimeOrderEngine.Client;
using RealTimeOrderEngine.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5211/") });
builder.Services.AddScoped<ProductApiService>();
builder.Services.AddScoped<OrderApiService>();
builder.Services.AddScoped<RealTimeOrderEngine.Client.Services.TableApiService>();

await builder.Build().RunAsync();