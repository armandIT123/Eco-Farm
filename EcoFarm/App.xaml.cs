using EcoFarm;
using Microsoft.Maui.Platform;
using EcoFarm.Handlers;

namespace EcoFarm;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderlessEntry), (handler, view) =>
        {
            if (view is BorderlessEntry)
            {
#if __ANDROID__
                handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());
#elif __IOS__
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            }
        });
        MainPage = new LoadingPage();
        CheckTokenAndNavigate();
    }

    private async void CheckTokenAndNavigate()
    {
        string tokenString = await SecureStorage.GetAsync("jwt_token_key");

        if (!string.IsNullOrEmpty(tokenString) && !IsTokenExpired(tokenString))
        {
            MainPage = new AppShell();
        }
        else
        {
            MainPage = new NavigationPage(new LoginPage());
        }
    }

    private bool IsTokenExpired(string tokenString)
    {
        try
        {
            // Parse the token.
            //  var token = new JwtSecurityToken(jwtEncodedString: tokenString);

            // Check if the token is expired.
            // return token.ValidTo < DateTime.UtcNow;
            return false;
        }
        catch
        {
            return true;
        }
    }

}
