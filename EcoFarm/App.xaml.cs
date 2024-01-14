using EcoFarm;
using Microsoft.Maui.Platform;
using Data;
using System.Collections.ObjectModel;

namespace EcoFarm;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new LoadingPage();

        GetData();
        CheckTokenAndNavigate();
    }

    private async Task GetData()
    {
        Task.Run(() =>
        {
            var service = ServiceHelper.GetService<IServiceLink>();
            service.GetSuppliers();
        });
       
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
