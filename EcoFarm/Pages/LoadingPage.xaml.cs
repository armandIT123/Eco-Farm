namespace EcoFarm;

public partial class LoadingPage : ContentPage
{
	public LoadingPage()
	{
		InitializeComponent();
       // StartRotationAnimation();
    }

    //private void StartRotationAnimation()
    //{
    //    // Define the rotation animation
    //    var animation = new Animation(v => RotatingImage.Rotation = v, 0, 360);

    //    // Set up the animation to repeat indefinitely
    //    animation.Commit(this, "RotationLoop", length: 1000, repeat: () => true);
    //}
}