using LawOf100.Features;
using Sparc.Platforms.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Sparcify();
builder.Services.AddScoped<IConfiguration>(_ => builder.Configuration);

builder.AddB2CApi<LawOf100Api>(
    "https://lawof100.onmicrosoft.com/9a4ea546-b5c9-4d27-918e-1029e60f7089/LawOf100.Access",
    builder.Configuration["ApiUrl"]);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToPage("/_Host");

app.Run();
