using Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using The49.Maui.BottomSheet;

namespace EcoFarm;

public class ProductBottomSheetViewModel : DataContextBase
{
    private Product product;
    private string supplierName;
    private int quantity = 1;
    private string buttonTextTemplate = "Adaugă în coș - {0} lei";

    public ProductBottomSheetViewModel()
	{
			
	}

    public byte[] Image => product?.Image;
    public string Name => product?.Name;
    public string Description => product?.Description;
    public decimal Price => product?.Price ?? 0;

    public int Quantity
    {
        get => quantity;
        set
        {
            quantity = value;
            
            OnPropertyChanged();
            OnPropertyChanged(nameof(ButtonText));
            OnPropertyChanged(nameof(IsDecreaseQuantityBtnEnabled));
        }
    }

    public string ButtonText => string.Format(buttonTextTemplate, Math.Round(quantity * Price, 2));

    public bool IsDecreaseQuantityBtnEnabled => quantity > 1;

    public ICommand ChangeQuantity => new CommandHelper<string>((param) =>
    {
        Quantity += param == "+" ? 1 : -1;
    });

    public ICommand AddToBasket => new CommandHelper((param) =>
    {
        var basketVM = ServiceHelper.GetService<BasketPageViewModel>();
        if(basketVM != null)
        {
            basketVM.AddToBasket(product, quantity, supplierName);
        }
    });

    public void DisplayProduct(Product product, string supplierName)
    {
        this.product = product;
        this.supplierName = supplierName;
        Quantity = 1;
        OnPropertyChanged(nameof(Image));
        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(Description));
        OnPropertyChanged(nameof(Price));
    }
}

public partial class ProductBottomSheet : BottomSheet
{
	private ProductBottomSheetViewModel viewModel;

	public ProductBottomSheet()
	{
		InitializeComponent();
        viewModel = new ProductBottomSheetViewModel();
        BindingContext = viewModel;
        HasBackdrop = true;
		CornerRadius = 15;
	}

    public void DisplayProduct(Product product, string supplierName)
    {
        viewModel.DisplayProduct(product, supplierName);
    }

    private void CloseButton_Clicked(object sender, EventArgs e)
    {
        this.DismissAsync();
    }
}