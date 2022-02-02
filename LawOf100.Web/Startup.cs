using LawOf100.Features._Plugins;
using Sparc.Authentication.SelfHosted;
using Sparc.Core;
using Sparc.Features;
using Sparc.Plugins.Database.Cosmos;

namespace LawOf100.Features;

public class Startup
{
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.Sparcify<Startup>()
            .AddSelfHostedAuthentication<LawOf100Authenticator>(Configuration["BaseUrl"], "Web", Configuration["WebClientUrl"]);

        services.AddScoped(typeof(IRepository<>), typeof(InMemoryRepository<>));
        services.AddRazorPages();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.Sparcify<Startup>(env);
        app.UseIdentityServer();
        app.UseDeveloperExceptionPage();
    }
}