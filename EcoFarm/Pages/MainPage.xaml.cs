using Data;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;

namespace EcoFarm;

public class HomeViewModel : DataContextBase
{

}

public partial class MainPage : ContentPage
{
    HomeViewModel viewModel;
    public MainPage()
    {
        InitializeComponent();
        viewModel = new HomeViewModel();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //Task.Run(() =>
        //{
        //    viewModel.GetSuppliersList();
        //});
    }
}
