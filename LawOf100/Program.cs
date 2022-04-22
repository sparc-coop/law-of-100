using LawOf100._Plugins;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using LawOf100.UI;

using Sparc.Authentication.AzureADB2C;
using Sparc.Kernel;
using Sparc.Notifications.Azure;
using Sparc.Core;
using Sparc.Plugins.Database.Cosmos;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args).Sparcify();

builder.Services.AddRazorPages();

builder.Services
    .AddCosmos<LawOf100Context>(builder.Configuration.GetConnectionString("Database"), "lawof100")
    .AddAzureADB2CAuthentication(builder.Configuration)
    .AddAzurePushNotifications(builder.Configuration.GetSection("Notifications"));

builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
builder.Services.AddScoped<SignOutSessionStateManager>();

var apiUrl = builder.Configuration["BaseUrl"] + "/";
var client = builder.Services.AddHttpClient("api").ConfigureHttpClient(x => x.BaseAddress = new Uri(apiUrl));

builder.Services.AddScoped(x =>
    (LawOf100Api)Activator.CreateInstance(typeof(LawOf100Api),
    "",
    x.GetService<IHttpClientFactory>().CreateClient("api")));

builder.Services.Replace(ServiceDescriptor.Scoped(typeof(IRepository<>), typeof(CosmosDbRepository<>)));


var app = builder.Build().Sparcify();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.Run();
