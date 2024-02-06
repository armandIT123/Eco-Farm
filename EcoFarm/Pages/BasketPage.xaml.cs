using Data;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EcoFarm;

public class BasketItems : DataContextBase
{
	public delegate void QuantityChanged(decimal changedAmount);

	public QuantityChanged quantityChanged;
	public Product Product { get; set; }
	public int Quantity { get; set; }
	public bool IsDecreaseBtnEnabled => Quantity > 1;
    public ICommand ChangeQuantity => new CommandHelper<string>((symbol) =>
    {
		int sign = (symbol == "+") ? 1 : -1;
		Quantity += sign;
		OnPropertyChanged(nameof(IsDecreaseBtnEnabled));
		OnPropertyChanged(nameof(Quantity));

        quantityChanged?.Invoke(sign * Product.Price);
    });
}

public class BasketPageViewModel : DataContextBase
{
	#region Members & Init
	private ObservableCollection<BasketItems> products;
	private string notes;
	private string supplierName;
	private decimal subtotal;

	public BasketPageViewModel()
	{
		products = new();
	}
	#endregion

    #region Accessors
    public ObservableCollection<BasketItems> Products => products;
	public bool AreItemsInBasket => products?.Count > 0;

	public string SupplierName
	{
		get => supplierName;
		set
		{
			supplierName = value;
			OnPropertyChanged();
		}
	}

	public string Notes
	{
		get => notes;
		set
		{
			notes = value;
			OnPropertyChanged();
		}
	}

	public decimal Subtotal
	{
		get => subtotal;
		set
		{
			subtotal = value;
			OnPropertyChanged();
			OnPropertyChanged(nameof(Total));
		}
	}
	public decimal DeliveryFee => 10;
	public decimal Total => subtotal + DeliveryFee;

	public ICommand DeleteProduct => new CommandHelper<int>((prodId) =>
	{
		var toBeDeleted = products.FirstOrDefault(x => x.Product.Id == prodId);
		if (toBeDeleted != null)
		{
			Subtotal = products.Count > 1 ? subtotal - toBeDeleted.Product.Price * toBeDeleted.Quantity : 0;
            products.Remove(toBeDeleted);
        }
        OnPropertyChanged(nameof(Products));
        OnPropertyChanged(nameof(AreItemsInBasket));
	});

    public ICommand GoToSupplier => new CommandHelper(async (n) =>
    {
        int supId = products[0]?.Product.SupplierId ?? 0;

        if (supId > 0)
        {
            await Shell.Current.GoToAsync(nameof(SupplierPage), new Dictionary<string, object>
            {
                {"CurrentSupplier", ServiceLink.Suppliers.FirstOrDefault(x => x.Id == supId) }
            });
        }
    });

    public ICommand GoToDiscoverPage => new CommandHelper( async (n) =>
	{
        await Shell.Current.GoToAsync($"///{nameof(DiscoverPage)}");
    });

	public ICommand GoToNextStep => new CommandHelper((n) =>
	{

	});
    #endregion

    #region Methods
    public void QuantityChanged(decimal changedAmount)
	{
		Subtotal += changedAmount;
	}

	public void AddToBasket(Product product, int quantity, string supplierName)
	{
		var prod = products.FirstOrDefault(x => x.Product.Id == product.Id);
		if (prod != null)
			prod.Quantity += quantity;
		else
		{
			BasketItems newProduct = new() { Product = product, Quantity = quantity };
			newProduct.quantityChanged += QuantityChanged;
            products.Add(newProduct);
        }

		SupplierName = supplierName;
        Subtotal += product.Price * quantity;

		OnPropertyChanged(nameof(AreItemsInBasket));
		OnPropertyChanged(nameof(Products));
		OnPropertyChanged(nameof(SupplierName));
	}
    #endregion
}

public partial class BasketPage : ContentPage
{
	private readonly BasketPageViewModel basketPageViewModel;
	public BasketPage(BasketPageViewModel viewModel)
	{
		InitializeComponent();
        basketPageViewModel = viewModel;
        BindingContext = basketPageViewModel;
	}
}