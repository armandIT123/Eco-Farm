namespace EcoFarm;

public partial class RatingDisplayer : ContentView
{
    public static readonly BindableProperty RatingProperty = BindableProperty.CreateAttached("Rating", typeof(double), typeof(RatingDisplayer), 0);
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
				// 3.5
				for(int i = 0; i < (int)Rating; i++)				
					ratingStarsImages.Add("FullStar.png");
				
				if (Rating < 5 && Rating - (int)Rating > 0.3)
					ratingStarsImages.Add("HalfStar.png");

				for(int i = 0; i < 5 - ratingStarsImages.Count; i++)				
					ratingStarsImages.Add("EmptyStar.png");				
			}
			OnPropertyChanged(nameof(RatingStarsImages));
        }
	}

	public List<string> RatingStarsImages => ratingStarsImages;
}