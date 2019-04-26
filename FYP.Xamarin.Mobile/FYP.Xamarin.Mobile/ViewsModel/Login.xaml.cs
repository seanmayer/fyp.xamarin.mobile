
using FYP.Xamarin.Mobile.Database;
using FYP.Xamarin.Mobile.Database.Model;
using FYP.Xamarin.Mobile.Database.Tables;
using FYP.Xamarin.Mobile.ViewModels;
using FYP.Xamarin.Mobile.ViewsModel;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FYP.Xamarin.Mobile
{
    public partial class Login : MasterDetailPage
    {
        private CredentialsCacheHandler credentialsCacheHandler;
        private AthleteCacheHandler athleteCacheHandler;
        private Credentials credentials;
        private Athlete athlete;

        public Login()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            credentialsCacheHandler = new CredentialsCacheHandler();
            athleteCacheHandler = new AthleteCacheHandler();
            IsGestureEnabled = false;
            InitilizeTapGestures();
        }

        private void InitilizeTapGestures()
        {
            ActivitiesList_Label.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => {
                    Detail = new NavigationPage(new ActivitieList(athlete.AthleteId.ToString(), athlete.StravaId, athlete.AccessToken));
                    ActivitiesList_Label.TextColor = Color.FromHex("#4285F4");
                    Trends_Label.TextColor = Color.FromHex("#000000");
                    Leaderboard_Label.TextColor = Color.FromHex("#000000");
                    Alerts_Label.TextColor = Color.FromHex("#000000");
                })
            }
);

            Trends_Label.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => {
                    Detail = new NavigationPage(new TrendMenu(athlete.AthleteId.ToString(), athlete.StravaId, athlete.AccessToken));
                    Trends_Label.TextColor = Color.FromHex("#4285F4");
                    ActivitiesList_Label.TextColor = Color.FromHex("#000000");
                    Leaderboard_Label.TextColor = Color.FromHex("#000000");
                    Alerts_Label.TextColor = Color.FromHex("#000000");
                })
            }
            );

            Leaderboard_Label.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => {
                    Detail = new NavigationPage(new LeaderboardMenu(athlete.AthleteId.ToString(), athlete.StravaId, athlete.AccessToken));
                    Leaderboard_Label.TextColor = Color.FromHex("#4285F4");
                    Trends_Label.TextColor = Color.FromHex("#000000");
                    ActivitiesList_Label.TextColor = Color.FromHex("#000000");
                    Alerts_Label.TextColor = Color.FromHex("#000000");
                })
            }
            );

            Alerts_Label.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => {
                    Detail = new NavigationPage(new ViewsModel.Alerts(athlete.AthleteId.ToString(), athlete.AccessToken));
                    Alerts_Label.TextColor = Color.FromHex("#4285F4");
                    Trends_Label.TextColor = Color.FromHex("#000000");
                    ActivitiesList_Label.TextColor = Color.FromHex("#000000");
                    Leaderboard_Label.TextColor = Color.FromHex("#000000");
                })
            }
            );
        }

        private void Signup_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Signup());
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            var isValid = CheckLoginCredentialsAsync();
            if (await isValid)
            {
                Athlete athlete = await athleteCacheHandler.Find(credentials.CredentialsId);
                this.athlete = athlete;
                IsGestureEnabled = true;
                Detail = new NavigationPage(new ActivitieList(athlete.AthleteId.ToString(), athlete.StravaId, athlete.AccessToken));
            }
            else
            {
                await DisplayAlert("Message", "Login Failed!", "OK");
            }
            await CheckLoginCredentialsAsync();
        }

        private async Task<bool> CheckLoginCredentialsAsync()
        {
            Credentials cred = await credentialsCacheHandler.Find(username.Text, password.Text);
            
            
            if (cred == null)
            {
                await DisplayAlert("Message", "Credentials Incorrect!", "OK");
                return false;
            }
            else
            {
                credentials = cred;
                //await DisplayAlert("Message", "Successful!", "OK");
                return true;
            }
        }


    }
}
