using Data;
using System.Collections.ObjectModel;

namespace EcoFarm;

public class DiscoverPageViewModel : DataContextBase
{
    #region Members & Init
    private ObservableCollection<Supplier> suppliers;
    private Supplier selectedSupplier;

    public DiscoverPageViewModel()
    {
    }
    #endregion

    #region Accessors
    public ObservableCollection<Supplier> Suppliers
    {
        get => suppliers;
        set
        {
            suppliers = value;
            OnPropertyChanged();
        }
    }

    public Supplier SelectedSupplier
    {
        get => selectedSupplier;
        set
        {
            selectedSupplier = value;
            
        }
    }
    #endregion

    #region Methods
    internal async Task GetSuppliersList()
    {
        var service = ServiceHelper.GetService<IServiceLink>();
        Suppliers = new ObservableCollection<Supplier>(await service.GetSuppliers() ?? new List<Supplier>());
    }
    #endregion
}

public partial class DiscoverPage : ContentPage
{
    DiscoverPageViewModel dataBinding = null;
	public DiscoverPage()
	{
		InitializeComponent();
        dataBinding = new DiscoverPageViewModel();
        BindingContext = dataBinding;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        Task.Run(() =>
        {
            dataBinding.GetSuppliersList();
        });
    }
}