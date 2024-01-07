using Data;
using System.Windows.Input;

namespace EcoFarm;

public enum SupplierButtonsEnum
{
    About = 0,
    Products = 1,
    Reviews = 2
}

public class SupplierPageViewModel : DataContextBase, IQueryAttributable
{
    #region Members & Init
    private Supplier currentSupplier = null;

    private string aboutFerm = string.Empty;
    private string selectedCategory;

    private List<string> pictures;
    private List<string> legume;
    private List<string> productsCategory;

    private SupplierButtonsEnum pressedButton = SupplierButtonsEnum.About;

    public SupplierPageViewModel()
    {
        AboutFerm = "   Produsele noastre sunt cultivate cu grijă și pasiune, asigurându-ți gustul autentic al alimentelor proaspete și sănătoase. Vizitează-ne pentru a te bucura de legume crocante, fructe zemoase și produse lactate cremoase, toate proaspete de la fermă. Experiența la ferma noastră nu este doar o escapadă, ci o călătorie înapoi la originile gustului. " +
            "Vino să descoperi frumusețea simplă a vieții la țară!";

        Pictures = new List<string>() { "ferm1.png", "ferm2.png", "ferm3.png" };
        Legume = new List<string>() { "ferm1.png", "ferm2.png", "ferm3.png"};
        ProductsCategory = new List<string>() { "Fructe", "Legume", "Cereale" };
    }
    #endregion

    #region Accessors
    public List<string> ProductsCategory
    {
        get => productsCategory;
        set
        {
            productsCategory = value;
            OnPropertyChanged(nameof(ProductsCategory));

        }
    }

    public string SelectedProductCategory
    {
        get => selectedCategory;
        set
        {
            if (selectedCategory != value)
            {
                selectedCategory = value;
                OnPropertyChanged(nameof(SelectedProductCategory));
            }
        }
    }

    public List<string> Legume
    {
        get => legume;
        set
        {
            legume = value;
            OnPropertyChanged();
        }
    }

    public List<string> Pictures
    {
        get => pictures;
        set
        {
            pictures = value;
            OnPropertyChanged();
        }
    }

    public string AboutFerm
    {
        get => aboutFerm;
        set
        {
            aboutFerm = value;
            OnPropertyChanged();
        }
    }

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
    public byte[] MainImage => CurrentSupplier?.Image;

    public SupplierButtonsEnum PressedButton
    {
        get => pressedButton;
        set
        {
            pressedButton = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsAboutSupplierVisible));
            OnPropertyChanged(nameof(IsProductSupplierVisible));
            OnPropertyChanged(nameof(IsReviewsSupplierVisible));
        }
    }

    public bool IsAboutSupplierVisible => PressedButton  == SupplierButtonsEnum.About;
    public bool IsProductSupplierVisible => PressedButton  == SupplierButtonsEnum.Products;
    public bool IsReviewsSupplierVisible => PressedButton  == SupplierButtonsEnum.Reviews;
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

    public ICommand PressedCategoriesCommand => new CommandHelper<string>((param) =>
    {
        SelectedProductCategory = param;
    });

    public ICommand PressedButtonsCommand => new CommandHelper<int>((param) =>
    {
        PressedButton = (SupplierButtonsEnum)param;
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