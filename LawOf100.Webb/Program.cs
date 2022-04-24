using Blazor.Analytics;
using LawOf100.Features;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sparc.Platforms.Web;

namespace LawOf100.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.Sparcify();

        builder.RootComponents.Add<App>("#app");

        builder.Services.AddScoped<IConfiguration>(_ => builder.Configuration);
        builder.Services.AddGoogleAnalytics("G-FQ2LH8DGH3");

        builder.AddB2CApi<LawOf100Api>(
            "https://lawof100.onmicrosoft.com/9a4ea546-b5c9-4d27-918e-1029e60f7089/LawOf100.Access",
            builder.Configuration["ApiUrl"]);

        await builder.Build().RunAsync();
    }
}