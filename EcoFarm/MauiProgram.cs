using Microsoft.Extensions.Logging;
using The49.Maui.BottomSheet;

namespace EcoFarm
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseBottomSheet()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            #region Pages
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();

            builder.Services.AddTransient<SupplierPage>();
            builder.Services.AddTransient<SupplierPageViewModel>();

            builder.Services.AddSingleton<DiscoverPage>();
            builder.Services.AddSingleton<DiscoverPageViewModel>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<HomeViewModel>();
            #endregion

            #region Core
            builder.Services.AddTransient<IPlatformHttpMessageHandler>(_ =>
            {
#if ANDROID
                return new Platforms.Android.AndroidHttpMessageHandler();
#else
                //return new Platforms.iOS.IosHttpMessageHandler();
                return null;
#endif
            });

            builder.Services.AddSingleton<IServiceLink, ServiceLink>();

            builder.Services.AddHttpClient("localHostClient", httpClient =>
            {
                string baseAdress = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7184" : "https://localhost:7184";
                httpClient.BaseAddress = new Uri(baseAdress);
            }).ConfigurePrimaryHttpMessageHandler(services =>
            {
                var platformHandler = services.GetService<IPlatformHttpMessageHandler>();
                return platformHandler.GetHttpMessageHandler();
            });
            #endregion
#if DEBUG
            builder.Logging.AddDebug();
#endif
            var app = builder.Build();
            ServiceHelper.Initialize(app.Services);


            return app;
        }

        //Maybe usefull
        private static void AddView<TView, TViewModel>(this IServiceCollection services)
            where TView : ContentPage, new()
        {
            services.AddSingleton<TView>(serviceProvider => new TView()
            {
                BindingContext = serviceProvider.GetRequiredService<TViewModel>()
            });
        }
    }
}
