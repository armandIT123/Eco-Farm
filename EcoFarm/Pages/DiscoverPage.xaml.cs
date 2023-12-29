using Data;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EcoFarm;

public class DiscoverPageViewModel : DataContextBase
{
    #region Members & Init
    private ObservableCollection<Supplier> suppliers;
    private Supplier selectedSupplier;
    private bool areFieldsEnabled = true;

    public DiscoverPageViewModel()
    {
        GetSuppliersList();
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

    public bool AreFieldsEnabled
    {
        get => areFieldsEnabled;
        set
        {
            areFieldsEnabled = value;
            OnPropertyChanged();
        }
    }

    public ICommand GoToSupplierPageCommand => new CommandHelper((param) =>
    {
        AreFieldsEnabled = false; 
        GoToSupplierPage(Convert.ToInt32(param));
    });
    #endregion

    #region Methods
    internal async Task GetSuppliersList()
    {
        var service = ServiceHelper.GetService<IServiceLink>();
        Suppliers = new ObservableCollection<Supplier>(await service.GetSuppliers() ?? new List<Supplier>());
    }

    internal async Task GoToSupplierPage(int supplierId)
    {
        await Shell.Current.GoToAsync(nameof(SupplierPage), new Dictionary<string, object>
        {
            {"CurrentSupplier", Suppliers.FirstOrDefault(x => x.Id == supplierId) }
        });
        AreFieldsEnabled = true;
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
        //Task.Run(() =>
        //{
        //    dataBinding.GetSuppliersList();
        //});
    }
}