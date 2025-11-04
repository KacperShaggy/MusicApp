using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Media; // jeśli używasz MediaElementa
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace MusicApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder

                .UseMauiApp<App>()
                .UseMauiCommunityToolkitCore()          // ✅ poprawna metoda dla MAUI 8/9
                .UseMauiCommunityToolkitMediaElement()  // ✅ jeśli chcesz odtwarzać audio/wideo
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            return builder.Build();
        }
    }
}
