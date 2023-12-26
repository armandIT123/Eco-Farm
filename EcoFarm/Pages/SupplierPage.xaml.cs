namespace EcoFarm;

public class SupplierPageViewModel : DataContextBase
{
	private SupplierPage currentSupplier = null;

	public SupplierPageViewModel()
	{
		
	}

	public SupplierPage CurrentSupplier
	{
		get => currentSupplier;
		set
		{
			currentSupplier = value;
			OnPropertyChanged();
            //Shell.Current.GoToAsync

        }
	}
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
}