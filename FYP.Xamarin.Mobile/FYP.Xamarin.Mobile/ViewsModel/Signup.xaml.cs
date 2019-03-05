using FYP.Xamarin.Mobile.Database;
using FYP.Xamarin.Mobile.Services;
using FYP.Xamarin.Mobile.Services.Model;
using FYP.Xamarin.Mobile.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FYP.Xamarin.Mobile
{
    public partial class Signup : ContentPage
    {
        private AthleteCacheHandler athleteCacheHandler;
        private CredentialsCacheHandler credentialsCacheHandler;
        private AthleteServiceHandler athleteServiceHandler;
        private CredentialsServiceHandler credentialsServiceHandler;

        public Signup()
        {
            InitializeComponent();
            athleteCacheHandler = new AthleteCacheHandler();
            credentialsCacheHandler = new CredentialsCacheHandler();
            athleteServiceHandler = new AthleteServiceHandler();
            credentialsServiceHandler = new CredentialsServiceHandler();
        }

        private async void ConnectToStrava_Clicked(object sender, EventArgs e)
        {
            if (await athleteServiceHandler.EstablishConnection())
            {
                long credId = athleteCacheHandler.CreatNewPK();
                long atheleteId = athleteCacheHandler.CreatNewPK();

                credentialsServiceHandler.CredentialList = await credentialsServiceHandler.FindAll();

                if (CheckEmptyFields(username.Text,password.Text,confirmPassword.Text, stravaId.Text, stravaApiKey.Text) == false 
                    && credentialsServiceHandler.CheckCredentialsUsernameExists(username.Text) == false)
                {
                    await CacheTransactionAsync(credId, atheleteId, await ServiceTransactionsAsync(credId, atheleteId));
                    await Navigation.PushAsync(new Login());
                    await DisplayAlert("Message", "You are Signed Up!", "OK");
                }
            }
            else
            {
                await DisplayAlert("Message", "No Connection with Server", "OK");
            }

        }

        public async Task<AthleteRootObject> ServiceTransactionsAsync(long credId, long athleteId)
        {
            credentialsServiceHandler.Init(credId.ToString(), username.Text, password.Text);
            await MessageAsync(await credentialsServiceHandler.Create());

            athleteServiceHandler.Init(athleteId.ToString(), credId.ToString(), stravaId.Text, stravaApiKey.Text);
            await MessageAsync(await athleteServiceHandler.Create());

            return await athleteServiceHandler.Find(athleteId.ToString());
        }

        public async Task CacheTransactionAsync(long credId, long athleteId, AthleteRootObject athleteRootObject)
        {
            credentialsCacheHandler.Init(credId, username.Text, password.Text);
            await MessageAsync(await credentialsCacheHandler.Create());

            athleteCacheHandler.Init(athleteId, credId, athleteRootObject.firstname, athleteRootObject.lastname, stravaId.Text, stravaApiKey.Text);
            await MessageAsync(await athleteCacheHandler.Create());
        }

        public async Task MessageAsync(bool process)
        {
            if(process == false)
            {
                await DisplayAlert("Message", "Something Went Wrong!", "OK");
            }
        }

        public bool CheckEmptyFields(string username, string password, string confirmPassword, string stravaId, string stravaApiKey)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(stravaId) || string.IsNullOrEmpty(stravaApiKey))
            {
                return true;
            }
            return false;
        }
    }
}