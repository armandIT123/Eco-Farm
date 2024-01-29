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
	private ObservableCollection<BasketItems> products;
	private string notes;
	private decimal subtotal;

	public BasketPageViewModel()
	{
		products = new();
	}

	public ObservableCollection<BasketItems> Products => products;
	public bool AreItemsInBasket => products?.Count > 0;

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

	public ICommand GoToSupplier => new CommandHelper(async (n) =>
	{
        await Shell.Current.GoToAsync(nameof(SupplierPage), new Dictionary<string, object>
        {
            {"CurrentSupplier", ServiceLink.Suppliers.FirstOrDefault(x => x.Id == 1) } // supplier Id
        });
    });

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

	public void QuantityChanged(decimal changedAmount)
	{
		Subtotal += changedAmount;
	}

	public void AddToBasket(Product product, int quantity)
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

        Subtotal += product.Price * quantity;

		OnPropertyChanged(nameof(AreItemsInBasket));
		OnPropertyChanged(nameof(Products));
	}
}

public partial class BasketPage : ContentPage
{
	private readonly BasketPageViewModel basketPageViewModel;
	public BasketPage(BasketPageViewModel viewModel)
	{
		InitializeComponent();
        //viewModel = ServiceHelper.GetService<BasketPageViewModel>();
        basketPageViewModel = viewModel;
        BindingContext = basketPageViewModel;
	}
}