using LawOf100.UI.Shared;
using Sparc.Platforms.Maui;

namespace LawOf100.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder().Sparcify<MainLayout>();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif

        return builder.Build();
    }
}