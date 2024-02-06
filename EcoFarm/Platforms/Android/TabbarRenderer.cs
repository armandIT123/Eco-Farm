//#region old
////using Android.Graphics.Drawables;
////using Android.OS;
////using Android.Views;
////using Android.Widget;
////using Google.Android.Material.BottomNavigation;
////using Microsoft.Maui.Controls.Handlers.Compatibility;
////using Microsoft.Maui.Controls.Platform;
////using Microsoft.Maui.Controls.Platform.Compatibility;
////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;
////using Button = Android.Widget.Button;
////using View = Android.Views.View;

////namespace EcoFarm.Platforms.Android;

////public class TabbarRenderer : ShellRenderer
////{
////    protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
////    {
////        // return base.CreateBottomNavViewAppearanceTracker(shellItem);
////        return new CustomShellBottomNavViewAppearanceTracker(this, shellItem);
////    }
////}

////class CustomShellBottomNavViewAppearanceTracker : ShellBottomNavViewAppearanceTracker
////{
////    public CustomShellBottomNavViewAppearanceTracker(IShellContext shellContext, ShellItem shellItem):base(shellContext, shellItem)
////    {    
////    }



////    public override void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
////    {
////        base.SetAppearance(bottomView, appearance);
////        bottomView.SetMinimumHeight(200);
////        var menuView = bottomView.GetChildAt(0) as BottomNavigationMenuView;
////        if (menuView != null)
////        {
////            var menu = bottomView.Menu;

////            if (menu.Size() >= 5)
////            {
////                // Assuming the middle item is at index 2
////                var menuItem = menu.GetItem(2);
////                var itemView = menuView.GetChildAt(2) as BottomNavigationItemView;

////                if(itemView != null)
////                {
////                    itemView.SetPadding(0, 0, 0, 25);

////                    var layoutParams = itemView.LayoutParameters as FrameLayout.LayoutParams;
////                    if(layoutParams != null)
////                    {
////                        layoutParams.Height *= 2; // Increase height by 20%
////                        layoutParams.Width *= 2;  // Increase width by 20%
////                                                  //layoutParams.Gravity = Gravity.Top; // Raise it to the top
////                        itemView.LayoutParameters = layoutParams;
////                    }




////                }

////            }
////        }


////    }
////}

////internal class CustomShellItemRenderer : ShellItemRenderer
////{
////    public CustomShellItemRenderer(IShellContext context) : base(context)
////    {
////    }

////    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
////    {
////        var view = base.OnCreateView(inflater, container, savedInstanceState);
////        if (Context is not null && ShellItem is CustomTabBar { CenterViewVisible: true } tabbar)
////        {
////            var rootLayout = new FrameLayout(Context)
////            {
////                LayoutParameters = new FrameLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent)
////            };

////            rootLayout.AddView(view);
////            const int middleViewSize = 150;
////            var middleViewLayoutParams = new FrameLayout.LayoutParams(
////                ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent,
////                GravityFlags.CenterHorizontal | GravityFlags.Bottom)
////            {
////                BottomMargin = 100,
////                Width = middleViewSize,
////                Height = middleViewSize
////            };
////            var middleView = new Button(Context)
////            {
////                LayoutParameters = middleViewLayoutParams
////            };
////            middleView.Click += delegate
////            {
////                tabbar.CenterViewCommand?.Execute(null);
////            };
////            middleView.SetText(tabbar.CenterViewText, TextView.BufferType.Normal);
////            middleView.SetPadding(0, 0, 0, 0);
////            if (tabbar.CenterViewBackgroundColor is not null)
////            {
////                var backgroundDrawable = new GradientDrawable();
////                backgroundDrawable.SetShape(ShapeType.Rectangle);
////                backgroundDrawable.SetCornerRadius(middleViewSize / 2f);
////                backgroundDrawable.SetColor(tabbar.CenterViewBackgroundColor.ToPlatform(Colors.Transparent));
////                middleView.SetBackground(backgroundDrawable);
////            }

////            tabbar.CenterViewImageSource?.LoadImage(Application.Current!.MainPage!.Handler!.MauiContext!, result =>
////            {
////                middleView.SetBackground(result?.Value);
////                middleView.SetMinimumHeight(0);
////                middleView.SetMinimumWidth(0);
////            });

////            rootLayout.AddView(middleView);
////            return rootLayout;
////        }

////        return view;
////    }
////}
//#endregion


//using Android.Content;
//using Android.Hardware.Lights;
//using Google.Android.Material.BottomNavigation;
//using Google.Android.Material.FloatingActionButton;
//using Microsoft.Maui.Controls;
//using Microsoft.Maui.Controls.Compatibility;
//using Microsoft.Maui.Controls.Compatibility.Platform.Android.AppCompat;
//using Microsoft.Maui.Controls.Platform;
//using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
//using MyMauiApp.Platforms.Android;
//using System.ComponentModel;
//using static Android.Icu.Text.ListFormatter;
//using AView = Android.Views.View;
//using TabbedPage = Microsoft.Maui.Controls.TabbedPage;

//[assembly: ExportRenderer(typeof(TabbedPage), typeof(CustomTabbedPageRenderer))]
//namespace MyMauiApp.Platforms.Android
//{
//    public class CustomTabbedPageRenderer : TabbedPageRenderer
//    {
//        FloatingActionButton _middleButton;

//        public CustomTabbedPageRenderer(Context context) : base(context)
//        {
//        }

//        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
//        {
//            base.OnElementChanged(e);

//            if (e.NewElement != null)
//            {
//                // Adding the FloatingActionButton
//                _middleButton = new FloatingActionButton(Context)
//                {
//                    LayoutParameters = new LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent),
//                    Size = FloatingActionButton.SizeNormal
//                };
//                _middleButton.SetImageResource(Resource.Drawable.your_icon); // Set your icon here
//                _middleButton.Click += MiddleButton_Click;

//                // You can customize your FAB here, for example, setting the background color
//                _middleButton.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.ParseColor("#your_color_hex"));

//                AddView(_middleButton);

//                // You might need to adjust the position of your button
//                _middleButton.TranslationY = -100; // Adjust this value as needed
//            }

//            if (e.OldElement != null)
//            {
//                _middleButton.Click -= MiddleButton_Click;
//            }
//        }

//        private void MiddleButton_Click(object sender, EventArgs e)
//        {
//            // Handle the FAB click event
//            // You could either manually switch to a specific tab, or invoke some other logic
//            ((TabbedPage)Element).CurrentPage = ((TabbedPage)Element).Children[2]; // This assumes the middle tab is at index 2
//        }

//        protected override void OnLayout(bool changed, int l, int t, int r, int b)
//        {
//            base.OnLayout(changed, l, t, r, b);

//            // This is to ensure that the FloatingActionButton does not interfere with the tab bar items
//            if (_middleButton != null)
//            {
//                // Calculate the translation needed to position the FAB in the center and above the tab bar
//                var centerX = (Width / 2) - (_middleButton.Width / 2);
//                var centerY = Height - (_middleButton.Height / 2) - 30; // 30 is an arbitrary value for the offset from the bottom, adjust as needed

//                _middleButton.BringToFront();
//                _middleButton.SetX(centerX);
//                _middleButton.SetY(centerY);
//            }
//        }

//        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
//        {
//            base.OnElementPropertyChanged(sender, e);

//            // If the tabs change, you might need to adjust the layout
//            if (e.PropertyName == nameof(TabbedPage.CurrentPage))
//            {
//                // Trigger layout update
//                this.RequestLayout();
//            }
//        }
//    }
//}

