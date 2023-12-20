using Data;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;

namespace EcoFarm;

public partial class MainPage : ContentPage
{
    List<Supplier> suppliers;

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;

        suppliers = new()
        {
            new Supplier()
            {
                Name = "Test",
                Image = "picture.png",
                Rating = 3.5,

            },
            new Supplier()
            {
                Name = "Test 2",
                Image = "picture.png",
                Rating = 5
            },
            new Supplier()
            {
                Name= "Test 3",
                Image = "picture.png",
                Rating = 4.5
            }
        };
        OnPropertyChanged(nameof(Suppliers));
    }

    public List<Supplier> Suppliers => suppliers;
    
}
