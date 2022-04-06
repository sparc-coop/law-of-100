using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace LawOf100.UI;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        await builder.Build().RunAsync();
    }
}