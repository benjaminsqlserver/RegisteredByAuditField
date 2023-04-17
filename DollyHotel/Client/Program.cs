using DollyHotel.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddHttpClient("DollyHotel.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
//   .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped<DollyHotel.Client.ConDataService>();
builder.Services.AddAuthorizationCore();
//builder.Services.AddHttpClient("DollyHotel.Server", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));



// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("DollyHotel.ServerAPI"));
builder.Services.AddHttpClient("ServerAPI.NoAuthenticationClient",
 client => client.BaseAddress = new
Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddApiAuthorization();

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

await builder.Build().RunAsync();
