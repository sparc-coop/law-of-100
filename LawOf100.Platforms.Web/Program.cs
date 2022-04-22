using Blazored.LocalStorage;
using LawOf100.UI;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sparc.Platforms.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args).Sparcify();

builder.AddB2CApi<LawOf100Api>(builder.Configuration["ApiUrl"]);

await builder.Build().RunAsync();