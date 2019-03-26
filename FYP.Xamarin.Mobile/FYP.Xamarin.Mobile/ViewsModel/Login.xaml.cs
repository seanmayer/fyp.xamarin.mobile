
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
    public partial class Login : ContentPage
    {
        private CredentialsCacheHandler credentialsCacheHandler;
        private AthleteCacheHandler athleteCacheHandler;
        private Credentials credentials;

        public Login()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            credentialsCacheHandler = new CredentialsCacheHandler();
            athleteCacheHandler = new AthleteCacheHandler();
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
                Navigation.InsertPageBefore(new ActivitieList(athlete.AthleteId.ToString(), athlete.StravaId.ToString(), athlete.AccessToken.ToString()), this);
                await Navigation.PopAsync();
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
