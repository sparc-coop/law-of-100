using LawOf100;
using LawOf100._Plugins;

#if !DEBUG
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
#endif

using Sparc.Authentication.AzureADB2C;
using Sparc.Kernel;
using Sparc.Notifications.Azure;

var builder = WebApplication.CreateBuilder(args).Sparcify();

builder.Services.AddRazorPages();

builder.Services
    .AddCosmos<LawOf100Context>(builder.Configuration.GetConnectionString("Database"), "lawof100")
    .AddAzureADB2CAuthentication(builder.Configuration)
    .AddAzurePushNotifications(builder.Configuration.GetSection("Notifications"));

#if !DEBUG
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
builder.Services.AddScoped<SignOutSessionStateManager>();

var client = builder.Services.AddHttpClient("api");

builder.Services.AddScoped(x =>
    (LawOf100Api)Activator.CreateInstance(typeof(LawOf100Api),
    builder.WebHost.GetSetting(WebHostDefaults.ServerUrlsKey),
    x.GetService<IHttpClientFactory>().CreateClient("api")));
#endif

var app = builder.Build().Sparcify();

app.Run();
