using Data;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EcoFarm;

public class SearchData
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class DiscoverPageViewModel : DataContextBase
{
    #region Members & Init
    private ObservableCollection<Supplier> suppliers;
    private ObservableCollection<SearchData> searchResults;

    private Supplier selectedSupplier;
    private string searchTerm = string.Empty;
    private bool areFieldsEnabled = true;
    private bool isSearchMode = false;

    public DiscoverPageViewModel()
    {
        ServiceLink.SuppliersUpdated += GetSuppliersList;
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

    public bool IsSearchMode
    {
        get => isSearchMode;
        set
        {
            isSearchMode = value;
            SearchResults = value ? new() : null;
            OnPropertyChanged();
        }
    }

    public string SearchTerm
    {
        get => searchTerm;
        set
        {
            if (searchTerm != value)
            {
                bool deletedChar = searchTerm.Length > value.Length;
                searchTerm = value;
                if (ServiceLink.Suppliers != null)
                {
                    SearchForSuppliers(deletedChar);
                }
            }
            OnPropertyChanged();
        }
    }

    public ObservableCollection<SearchData> SearchResults
    {
        get => searchResults;
        set
        {
            searchResults = value;
            OnPropertyChanged();
        }
    }

    public ICommand GoToSupplierPageCommand => new CommandHelper<int>( async (param) =>
    {
        AreFieldsEnabled = false;
        await GoToSupplierPage(param);
    });

    public ICommand SearchCommand => new CommandHelper((a) =>
    {
        Suppliers.Clear();
        foreach(var supplierName in SearchResults)
        {
            var supplier = ServiceLink.Suppliers?.FirstOrDefault(x => x.Name == supplierName.Name);
            if (supplier != null)
                Suppliers.Add(supplier);
        }
        isSearchMode = false;
        OnPropertyChanged(nameof(IsSearchMode));
    });

    public ICommand SearchBarPressed => new CommandHelper<string>((state) =>
    {
        IsSearchMode = false;
        SearchTerm = string.Empty;
        GetSuppliersList();
    });
    #endregion

    #region Methods
    private void SearchForSuppliers(bool deletedChar)
    {
        if (deletedChar)
        {
            if (searchTerm == string.Empty)
            {
                searchResults?.Clear();
                OnPropertyChanged(nameof(SearchTerm));
                return;
            }
            else
            {
                foreach (var supplier in ServiceLink.Suppliers)
                {
                    if (supplier.Name.ToLower().Replace(" ", "").Contains(searchTerm.ToLower().Replace(" ", "")) && !searchResults.Any(x => x.Id == supplier.Id))
                        searchResults.Add(new SearchData() { Id = supplier.Id, Name = supplier.Name });
                }
            }
        }
        else
        {
            if (searchResults.Count == 0)
            {
                foreach (var supplier in ServiceLink.Suppliers)
                { //to do prioritise those that starts with the search term, then those that contain it
                    if (supplier.Name.ToLower().Replace(" ", "").Contains(searchTerm.ToLower().Replace(" ", "")))
                        searchResults.Add(new SearchData() { Id = supplier.Id, Name = supplier.Name });
                }
            }
            else
            {
                for (int i = searchResults.Count - 1; i >= 0; i--)
                {
                    if (!searchResults[i].Name.ToLower().Replace(" ", "").Contains(searchTerm.ToLower().Replace(" ", "")))
                        searchResults.RemoveAt(i);
                }
            }
        }
        OnPropertyChanged(nameof(SearchResults));
    }
   
    internal void GetSuppliersList()
    {
        suppliers = new ObservableCollection<Supplier>();
        if (ServiceLink.Suppliers != null)
        {
            foreach(var supplier in ServiceLink.Suppliers)
            {
                Suppliers.Add(supplier);
            }
        }
        OnPropertyChanged(nameof(Suppliers));
    }

    internal async Task GoToSupplierPage(int supplierId)
    {
        await Shell.Current.GoToAsync(nameof(SupplierPage), new Dictionary<string, object>
        {
            {"CurrentSupplier", ServiceLink.Suppliers.FirstOrDefault(x => x.Id == supplierId) }
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

    //private void SearchBar_Focused(object sender, FocusEventArgs e)
    //{
    //    dataBinding.IsSearchMode = true;
    //}
}