using FYP.Xamarin.Mobile.Database;
using FYP.Xamarin.Mobile.Database.Model;
using FYP.Xamarin.Mobile.Database.Tables;
using FYP.Xamarin.Mobile.IServices;
using FYP.Xamarin.Mobile.Json;
using FYP.Xamarin.Mobile.Services.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FYP.Xamarin.Mobile
{
    public partial class Signup : ContentPage, IServerServices
    {
        private List<CredentialsRootObject> credentialList;
        private List<AthleteRootObject> athleteList;
        private bool connection = false;
        private DatabaseHandler<Credentials> credentials_DbHandler;
        private DatabaseHandler<Athlete> athlete_DbHandler;

        public Signup()
        {
            InitializeComponent();
            EstablishConnectionAsync();
            credentials_DbHandler = new DatabaseHandler<Credentials>();
        }

        public async void InsertCredentials()
        {
            Credentials cred = new Credentials(username.Text, password.Text);
            await credentials_DbHandler.Insert(cred);
        }

        public async void InsertAthlete(Credentials cred, string firstName, string lastName)
        {
            Athlete athlete = new Athlete(cred,stravaId.Text,firstName,lastName);
            await athlete_DbHandler.Insert(athlete);
        }

        public async void GetCredentialsList()
        {
            var credentials = await credentials_DbHandler.Get<Credentials>();

            foreach (Credentials c in credentials)
            {
                await DisplayAlert("Message", c.Username, "OK");
            }
        }

        private async void ConnectToStrava_Clicked(object sender, EventArgs e)
        {
            EstablishConnectionAsync();


            if (connection == true)
            {
                this.credentialList = await FindAllCredentials();
                //this.athleteList = await FindAllAthletes();

               
                //await DisplayAlert("Message", GetNewAthleteId(), "OK");


                if (CheckEmptyFields() == false && CheckCredentialsUsernameExists(username.Text) == false)
                {
                    await CreateCredentialsService("1", username.Text, password.Text);
                    await CreateAthleteService(GetNewCredendentialsId(), stravaId.Text, stravaApiKey.Text);
                    //InsertCredentials();
                    
                    //this.athleteList = await FindAllAthletes();
                    //InsertAthlete(new Credentials(username.Text, password.Text), "", "");
                    await DisplayAlert("Message", "You are Signed Up!", "OK");
                }
            }
            else
            {
                await DisplayAlert("Message", "No Connection with Server", "OK");
            }

        }

        public async void EstablishConnectionAsync()
        {
            var client = new HttpClient(); 
            HttpResponseMessage response = await client.GetAsync(RequestFactory.GetSingleton().PROJECT_PACKAGE);
            CheckResponseCode(response);
        }

        public async void CheckResponseCode(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Message", "Connected", "OK");
                connection = true;
            }
        }

        public bool CheckEmptyFields()
        {
            if (string.IsNullOrEmpty(username.Text) || string.IsNullOrEmpty(password.Text) || string.IsNullOrEmpty(confirmPassword.Text) || string.IsNullOrEmpty(stravaId.Text) || string.IsNullOrEmpty(stravaApiKey.Text))
            {
                DisplayAlert("Validation Error", "Please fill in all fields", "OK");
                return true;
            }
            return false;
        }

        public bool CheckCredentialsUsernameExists(string username)
        {
            bool exists = credentialList.Any(cred => cred.username == username);
            if (exists == true){DisplayAlert("Validation Error", "Username taken", "OK"); return exists;}
            else{return exists;} 
        }

        public async Task CreateAthleteService(string credentialID, string stravaId, string accessToken)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(RequestFactory.GetSingleton().CREATE_ATHLETE + credentialID +"/"+stravaId+ "/"+accessToken)
            };
            HttpResponseMessage response = await client.GetAsync("");
        }

        public async Task CreateCredentialsService(string credentialId, string username, string password)
        {
            JObject jsonObject = new JObject(new JProperty("credentialsId", credentialId),new JProperty("username", username),new JProperty("password", password));

            var content = new StringContent(JsonConvert.SerializeObject(jsonObject), System.Text.Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(new Uri(RequestFactory.GetSingleton().CREATE_CREDENTIALS), content);
            string responseBody = await response.Content.ReadAsStringAsync();
        }

        public async Task<List<CredentialsRootObject>> FindAllCredentials()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(RequestFactory.GetSingleton().LIST_CREDENTIALS)
            };
            HttpResponseMessage response = await client.GetAsync("");

            return JsonConvert.DeserializeObject<List<CredentialsRootObject>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<AthleteRootObject>> FindAllAthletes()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(RequestFactory.GetSingleton().LIST_ATHLETE)
            };
            HttpResponseMessage response = await client.GetAsync("");

            return JsonConvert.DeserializeObject<List<AthleteRootObject>>(await response.Content.ReadAsStringAsync());
        }

        public string GetNewCredendentialsId()
        {
            return (credentialList.OrderByDescending(cred => cred.credentialsId).FirstOrDefault().credentialsId + 1).ToString();
        }

        public string GetNewAthleteId()
        {
            return (athleteList.OrderByDescending(athlete => athlete.AthleteId).FirstOrDefault().AthleteId + 1).ToString();
        }

        public string GetJsonMessage(HttpResponseMessage response)
        {
            return (string)JObject.Parse(new StreamReader(response.Content.ReadAsStreamAsync().Result).ReadToEnd()).SelectToken("message");
        }
    }
}