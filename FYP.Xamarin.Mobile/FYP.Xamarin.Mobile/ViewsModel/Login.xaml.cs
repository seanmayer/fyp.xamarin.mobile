
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
            await CheckLoginCredentialsAsync();
        }

        private async Task CheckLoginCredentialsAsync()
        {
            Credentials cred = await credentialsCacheHandler.Find(username.Text, password.Text);
            
            
            if (cred == null)
            {
                await DisplayAlert("Message", "Credentials Incorrect!", "OK");
            }
            else
            {
                Athlete athlete = await athleteCacheHandler.Find(cred.CredentialsId);
                await Navigation.PushAsync(new ActivitieList(athlete.AthleteId.ToString()));
                await DisplayAlert("Message", "Successful!", "OK");
            }
        }

    }
}
