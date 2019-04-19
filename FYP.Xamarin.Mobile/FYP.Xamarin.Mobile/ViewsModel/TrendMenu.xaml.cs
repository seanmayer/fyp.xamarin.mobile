using FYP.Xamarin.Mobile.Database.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP.Xamarin.Mobile.ViewsModel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrendMenu : ContentPage
    {
        private string AthleteId;
        private string StravaId;
        private string AccessToken;
        private string buttonDefinition1 = "Power";
        private string buttonDefinition2 = "Cadence";
        private string buttonDefinition3 = "Speed";

        public TrendMenu(string athleteId, string stravaId, string accessToken)
        {
            InitializeComponent();
            Title = "Trend";
            this.AccessToken = accessToken;
            this.StravaId = stravaId;
            this.AthleteId = athleteId;
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
            await Navigation.PushAsync(new TrendAnalysis(AthleteId, StravaId, AccessToken, buttonDefinition1));
        }

        private async void ImageButton_Clicked_1(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new TrendAnalysis(AthleteId, StravaId, AccessToken, buttonDefinition2));
        }

        private async void ImageButton_Clicked_2(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new TrendAnalysis(AthleteId, StravaId, AccessToken, buttonDefinition3));
        }
    }
}