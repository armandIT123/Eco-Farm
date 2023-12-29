using System.Windows.Input;

namespace EcoFarm;

public partial class LargeImageBox : ContentView
{
    #region Memebers & Init
    public static readonly BindableProperty MainImageProperty = BindableProperty.Create(nameof(MainImage), typeof(string), typeof(LargeImageBox), "",
        propertyChanged: OnImagePropertyChanged);
    public static readonly BindableProperty SupplierNameProperty = BindableProperty.Create(nameof(SupplierName), typeof(string), typeof(LargeImageBox), "",
        propertyChanged: OnNamePropertyChanged);
    public static readonly BindableProperty RatingProperty = BindableProperty.Create(nameof(Rating), typeof(double), typeof(LargeImageBox), 0.0,
        propertyChanged: OnRatingPropertyChanged);
    public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(LargeImageBox));
    public static readonly BindableProperty TapCommandParameterProperty = BindableProperty.Create(nameof(TapCommandParameter), typeof(object), typeof(LargeImageBox));

    private bool isCategoryVisible = true;

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

    public bool IsCategoryVisible
    {
        get => isCategoryVisible;
        set
        {
            isCategoryVisible = value;
            OnPropertyChanged();
        }
    }

    public ICommand TapCommand
    {
        get => (ICommand)GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }

    public object TapCommandParameter
    {
        get => GetValue(TapCommandParameterProperty);
        set => SetValue(TapCommandParameterProperty, value);
    }
    #endregion

    #region Events
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

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        TapCommand?.Execute(TapCommandParameter);
    }
    #endregion
}