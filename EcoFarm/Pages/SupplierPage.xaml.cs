using Data;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EcoFarm;



public class SupplierPageViewModel : DataContextBase, IQueryAttributable
{
    public enum SupplierButtonsEnum
    {
        About = 0,
        Products = 1,
        Reviews = 2
    }

    #region Members & Init
    private Supplier currentSupplier = null;
    private ObservableCollection<Product> products;
    private ObservableCollection<Review> reviews;
    private SupplierAbout info;
    private string? selectedCategory;

    private SupplierButtonsEnum pressedButton = SupplierButtonsEnum.About;

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

    public ObservableCollection<Product>? DisplayedProducts
    {
        get
        {
            products ??= new();
            return new(products.Where(x => x.Category == selectedCategory));
        }
    }
    public string SupplierName => CurrentSupplier?.Name;
    public double Rating => CurrentSupplier?.Rating ?? 0;
    public byte[] MainImage => CurrentSupplier?.Image;


    public List<string?> ProductsCategory => products?.Select(x => x.Category)?.Distinct().ToList();

    public string? SelectedProductCategory
    {
        get => selectedCategory;
        set
        {
            if (selectedCategory != value)
            {
                selectedCategory = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayedProducts));
            }
        }
    }
   
    public string Description => info?.Description;
    public byte[][] Pictures => info?.Images;

    public SupplierButtonsEnum PressedButton
    {
        get => pressedButton;
        set
        {
            if(pressedButton != value)
            {
                pressedButton = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsAboutSupplierVisible));
                OnPropertyChanged(nameof(IsProductSupplierVisible));
                OnPropertyChanged(nameof(IsReviewsSupplierVisible));
            }
        }
    }

    public bool IsAboutSupplierVisible => PressedButton == SupplierButtonsEnum.About;
    public bool IsProductSupplierVisible => PressedButton == SupplierButtonsEnum.Products;
    public bool IsReviewsSupplierVisible => PressedButton == SupplierButtonsEnum.Reviews;

    public ICommand GoBackCommand => new CommandHelper( async (param) =>
    {
        await GoBack();
    });

    public ICommand PressedCategoriesCommand => new CommandHelper<string>((param) =>
    {
        SelectedProductCategory = param;
    });

    public ICommand PressedButtonsCommand => new CommandHelper<int>((param) =>
    {
        PressedButton = (SupplierButtonsEnum)param;
    });
    #endregion

    #region Methods
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("CurrentSupplier"))
        {
            CurrentSupplier = query["CurrentSupplier"] as Supplier;
        }
    }

    public async Task GetSupplierProducts()
    {
        var service = ServiceHelper.GetService<IServiceLink>();
        products = new ObservableCollection<Product>( await service.GetProducts(CurrentSupplier?.Id ?? 0) );

        OnPropertyChanged(nameof(DisplayedProducts));
        OnPropertyChanged(nameof(ProductsCategory));
        SelectedProductCategory = ProductsCategory?.Count > 0 ? ProductsCategory[0] : "";
    }

    public async Task GetSupplierInfo()
    {
        var service = ServiceHelper.GetService<IServiceLink>();
        info = await service.GetSupplierDesciption(CurrentSupplier?.Id ?? 0);
        OnPropertyChanged(nameof(Pictures));
        OnPropertyChanged(nameof(Description));
    }

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
        dataContext?.GetSupplierInfo();
        dataContext?.GetSupplierProducts();
    }
}