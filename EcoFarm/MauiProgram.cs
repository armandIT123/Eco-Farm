using Microsoft.Extensions.Logging;

namespace EcoFarm
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            //To do, build httpClient here
           // builder.Services.AddHttpClient("core", httpclient => httpclient.BaseAddress = new Uri("https://localhost:7184/WeatherForecast"));
            return builder.Build();
        }
    }
}
