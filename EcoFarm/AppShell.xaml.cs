namespace EcoFarm
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SupplierPage), typeof(SupplierPage));
            Routing.RegisterRoute(nameof(DiscoverPage), typeof(DiscoverPage));
        }
    }
}
