using Data;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EcoFarm;

public class LoginViewModel : DataContextBase
{
    private string email;
    private string password;
    private string name;


    public string Email
    {
        get => email;
        set
        {
            email = value;
        }
    }

    public string Password
    {
        private get => password; //maybe no get at all
        set
        {
            password = value;
        }
    }

    public string Name
    {
        get => name;
        set
        {
            name = value;
        }
    }

    public ICommand Skip => new CommandHelper((param) =>
    {
        if (App.Current == null)
            ;
        App.Current.MainPage = new AppShell();
    });

    internal async Task RegisterUser() //maybe move to register page
    {
        var service = ServiceHelper.GetService<IServiceLink>();
        RegisterDTO registerDTO = new()
        {
            Email = email,
            Password = password,
            Name = name
        };

        string responseMessage = await service.RegisterUser(registerDTO);
        await SecureStorage.SetAsync(responseMessage, "jwt_token_key");
    }


}

public partial class LoginPage : ContentPage
{
    LoginViewModel viewModel = null;

    public LoginPage()
    {
        InitializeComponent();
        viewModel = new LoginViewModel();
        BindingContext = viewModel;
    }
}