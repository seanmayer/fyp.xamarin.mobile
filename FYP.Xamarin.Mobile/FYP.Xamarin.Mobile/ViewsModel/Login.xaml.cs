
using FYP.Xamarin.Mobile.Database;
using FYP.Xamarin.Mobile.Database.Tables;
using FYP.Xamarin.Mobile.ViewsModel;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FYP.Xamarin.Mobile
{
    public partial class Login : ContentPage
    {
        private CredentialsCacheHandler credentialsCacheHandler;

        public Login()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            credentialsCacheHandler = new CredentialsCacheHandler();

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
                await Navigation.PushAsync(new ActivitieList());
                await DisplayAlert("Message", "Successful!", "OK");
            }
        }

    }
}
