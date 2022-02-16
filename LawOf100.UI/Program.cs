using LawOf100.Features;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sparc.Authentication.Blazor;

namespace LawOf100.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.RootComponents.Add<App>("#app");

        builder.Services.AddScoped<IConfiguration>(_ => builder.Configuration);

        builder.AddB2CApi<LawOf100Api>(
            "https://kodekitui.onmicrosoft.com/ccba7246-6276-4566-a964-12d7a2b48198/KodekitAPI.Access",
            "https://localhost:7147");

        await builder.Build().RunAsync();
    }
}