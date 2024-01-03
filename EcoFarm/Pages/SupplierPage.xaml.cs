using Data;
using System.Windows.Input;

namespace EcoFarm;

public class SupplierPageViewModel : DataContextBase, IQueryAttributable
{
    #region Members & Init
    private Supplier currentSupplier = null;

    public SupplierPageViewModel()
    {

    }
    #endregion

    #region Accessors
    public Supplier CurrentSupplier
    {
        get => currentSupplier;
        set
        {
            currentSupplier = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(SupplierName));
            OnPropertyChanged(nameof(Rating));
            OnPropertyChanged(nameof(MainImage));
        }
    }

    public string SupplierName => CurrentSupplier?.Name;

    public double Rating => CurrentSupplier?.Rating ?? 0;
    public string MainImage => CurrentSupplier?.Image;


    #endregion

    #region Methods
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("CurrentSupplier"))
        {
            CurrentSupplier = query["CurrentSupplier"] as Supplier;
        }
    }

    public ICommand GoBackCommand => new CommandHelper((param) =>
    {
        GoBack();
    });

    async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    #endregion
}

public partial class SupplierPage : ContentPage
{
    private SupplierPageViewModel dataContext = null;

    public SupplierPage()
    {
        InitializeComponent();
        dataContext = new SupplierPageViewModel();
        BindingContext = dataContext;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}