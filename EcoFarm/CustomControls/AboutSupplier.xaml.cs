namespace EcoFarm;

public class AboutSupplierViewModel : DataContextBase
{
    public string Text { get; set; }

    public AboutSupplierViewModel()
    {
        Text = "ceva";
    }
}

public partial class AboutSupplier : ContentView
{
    public AboutSupplier()
    {
        InitializeComponent();
        BindingContext = new AboutSupplierViewModel();
    }
}