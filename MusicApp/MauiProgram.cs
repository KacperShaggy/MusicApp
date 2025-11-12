using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace MusicApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Poppins-Light.ttf", "Poppins-Light");
                fonts.AddFont("Poppins-Bold.ttf", "Poppins-Bold");
            });

        return builder.Build();
    }
}
