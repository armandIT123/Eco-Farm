namespace EcoFarm;

public partial class LargeImageBox : ContentView
{
    #region Memebers & Init
    public static readonly BindableProperty MainImageProperty = BindableProperty.CreateAttached("MainImage", typeof(string), typeof(LargeImageBox), string.Empty);
    public static readonly BindableProperty SupplierNameProperty = BindableProperty.CreateAttached("SupplierName", typeof(string), typeof(LargeImageBox), string.Empty);
    public static readonly BindableProperty RatingProperty = BindableProperty.CreateAttached("Rating", typeof(int), typeof(LargeImageBox), 0);

    public LargeImageBox()
	{
		InitializeComponent();
		BindingContext = this;
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

    public int Rating
    {
        get => (int)GetValue(RatingProperty);
        set => SetValue(RatingProperty, value);
    }
    #endregion
}