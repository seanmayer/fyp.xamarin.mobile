using FYP.Xamarin.Mobile.Database.Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP.Xamarin.Mobile.ViewsModel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActivityMenu : ContentPage
	{
        private Activity Activity;
        private string AccessToken;
        private string buttonDefinition1 = "Power";
        private string buttonDefinition2 = "Cadence";
        private string buttonDefinition3 = "Speed";

        public ActivityMenu()
		{
			InitializeComponent();
            Title = Activity.name;
            ApplyStyles();
        }

        public ActivityMenu(Activity activity, string accessToken)
        {
            InitializeComponent();
            Title = activity.name;
            this.Activity = activity;
            this.AccessToken = accessToken;
            ApplyStyles();
        }


        public void ApplyStyles()
        {
            BackgroundColor = Color.FromHex("#1F2D44");
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#1F2D44");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }

        private async void ImageButton_Clicked(object sender, System.EventArgs e)
        {
             await Navigation.PushAsync(new ActivityAnaylsis(Activity, AccessToken, buttonDefinition1));
        }

        private async void ImageButton_Clicked_1(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ActivityAnaylsis(Activity, AccessToken, buttonDefinition2));
        }

        private async void ImageButton_Clicked_2(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ActivityAnaylsis(Activity, AccessToken, buttonDefinition3));
        }

    }
}