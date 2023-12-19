using System.Collections.ObjectModel;

namespace EcoFarm;

public partial class MainPage : ContentPage
{
    //ObservableCollection<Supplier> suppliers

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    //ObservableCollection<Supplier> Suppliers => suppliers
}
