using FYP.Xamarin.Mobile.IServices;
using FYP.Xamarin.Mobile.Json;
using FYP.Xamarin.Mobile.Services.Model;
using FYP.Xamarin.Mobile.Services.RequestFactory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Xamarin.Mobile.Services
{
    public class AthleteServiceHandler : IServerServices<AthleteRootObject>
    {
        private string AthleteId;
        private string CredId;
        private string StravaId;
        private string AccessToken;
        public List<AthleteRootObject> AthleteList { get; set; }

        public void Init(string athleteId, string credId, string stravaId, string accessToken)
        {
            this.AthleteId = athleteId;
            this.CredId = credId;
            this.StravaId = stravaId;
            this.AccessToken = accessToken;
        }

        public async Task<bool> EstablishConnection()
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(RequestFactory.RequestFactory.GetSingleton().PROJECT_PACKAGE);
            return CheckResponseCode(response);
        }
        public bool CheckResponseCode(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string GetJsonMessage(HttpResponseMessage response)
        {
            return (string)JObject.Parse(new StreamReader(response.Content.ReadAsStreamAsync().Result).ReadToEnd()).SelectToken("message");
        }

        
        public async Task<bool> Create()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(RequestFactory.RequestFactory.GetSingleton().CREATE_ATHLETE + AthleteId + "/"+ CredId + "/" + StravaId + "/" + AccessToken)
            };
            HttpResponseMessage response = await client.GetAsync("");
            return CheckResponseCode(response);
        }

        public async Task<AthleteRootObject> Find(string athleteID)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(RequestFactory.RequestFactory.GetSingleton().FIND_ATHLETE + athleteID)
            };
            HttpResponseMessage response = await client.GetAsync("");

            return JsonConvert.DeserializeObject<AthleteRootObject>(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<AthleteRootObject>> FindAll()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(RequestFactory.RequestFactory.GetSingleton().LIST_ATHLETE)
            };
            HttpResponseMessage response = await client.GetAsync("");

            return JsonConvert.DeserializeObject<List<AthleteRootObject>>(await response.Content.ReadAsStringAsync());
        }

        public string GetNewId()
        {
            return (AthleteList.OrderByDescending(athlete => athlete.athleteId).FirstOrDefault().athleteId).ToString();
        }




    }
}
