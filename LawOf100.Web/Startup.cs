using LawOf100.Features._Plugins;
using Sparc.Authentication.AzureADB2C;
using Sparc.Core;
using Sparc.Features;
using Sparc.Notifications.Azure;
using Sparc.Plugins.Database.Cosmos;

namespace LawOf100.Features;

public class Startup
{
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        var section = Configuration.GetSection("Notifications").Get<AzureConfiguration>();
        
        services.Sparcify<Startup>(Configuration["WebClientUrl"])
            .AddCosmos<LawOf100Context>(Configuration.GetConnectionString("Database"), "lawof100")
            .AddAzureADB2CAuthentication(Configuration)
            .AddAzurePushNotifications(Configuration.GetSection("Notifications"));

        services.AddScoped(typeof(IRepository<>), typeof(CosmosDbRepository<>));
        services.AddRazorPages();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.Sparcify<Startup>(env);
        app.UseDeveloperExceptionPage();
    }
}