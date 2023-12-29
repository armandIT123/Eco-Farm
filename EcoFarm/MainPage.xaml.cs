using Data;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;

namespace EcoFarm;

public partial class MainPage : ContentPage
{
    DiscoverPageViewModel viewModel;
    public MainPage()
    {
        InitializeComponent();
        viewModel = new DiscoverPageViewModel();
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
