namespace EcoFarm;

public partial class LargeImageBox : ContentView
{
    #region Memebers & Init
    public static readonly BindableProperty MainImageProperty = BindableProperty.CreateAttached(nameof(MainImage), typeof(string), typeof(LargeImageBox), "",
        propertyChanged: OnImagePropertyChanged);
    public static readonly BindableProperty SupplierNameProperty = BindableProperty.CreateAttached(nameof(SupplierName), typeof(string), typeof(LargeImageBox), "",
        propertyChanged: OnNamePropertyChanged);
    public static readonly BindableProperty RatingProperty = BindableProperty.CreateAttached(nameof(Rating), typeof(double), typeof(LargeImageBox), 0.0,
        propertyChanged: OnRatingPropertyChanged);

    public LargeImageBox()
	{
		InitializeComponent();
		
	}
    #endregion

    #region Accessors
    public string MainImage
    {
        get => (string)GetValue(MainImageProperty);
        set => SetValue(MainImageProperty, value);
    }

    public string SupplierName
    {
        get => (string)GetValue(SupplierNameProperty);
        set => SetValue(SupplierNameProperty, value);
    }

    public double Rating
    {
        get => (double)GetValue(RatingProperty);
        set => SetValue(RatingProperty, value);
    }
    #endregion

    #region Property Changed
    private static void OnNamePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (LargeImageBox)bindable;
        control.SupplierName = (string)newValue;
    }

    private static void OnImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (LargeImageBox)bindable;
        control.MainImage = (string)newValue;
    }

    private static void OnRatingPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (LargeImageBox)bindable;
        control.Rating = (double)newValue;
    }
    #endregion
}