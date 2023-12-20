namespace EcoFarm;

public partial class RatingDisplayer : ContentView
{
    public static readonly BindableProperty RatingProperty = BindableProperty.CreateAttached(nameof(Rating), typeof(double), typeof(RatingDisplayer), 0.0,
		propertyChanged: OnRatingPropertyChanged);

	private List<string> ratingStarsImages;

    public RatingDisplayer()
	{
		InitializeComponent();
		ratingStarsImages = new();
	}

	public double Rating
	{
		get => (double)GetValue(RatingProperty);
		set
		{
            SetValue(RatingProperty, value);
			if(Rating > 0)
			{				
				for(int i = 0; i < (int)Rating; i++)				
					ratingStarsImages.Add("fullstar.png");
				

				if (Rating - (int)Rating > 0.3)
					ratingStarsImages.Add("halfstar.png");

				if(ratingStarsImages.Count != 5)
					for(int i = 0; i <= 5 - ratingStarsImages.Count; i++)				
						ratingStarsImages.Add("emptystar.png");				
			}
			OnPropertyChanged(nameof(RatingStarsImages));
        }
	}

	public List<string> RatingStarsImages => ratingStarsImages;

    private static void OnRatingPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (RatingDisplayer)bindable;
        control.Rating = (double)newValue;
    }
}