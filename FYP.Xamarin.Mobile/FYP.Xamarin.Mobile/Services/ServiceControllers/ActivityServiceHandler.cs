using FYP.Xamarin.Mobile.IServices;
using FYP.Xamarin.Mobile.Services.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Xamarin.Mobile.Services
{
    public class ActivityServiceHandler : IServerServices<ActivityRootObject>
    {
        private string AthleteId;
        private string StravaId;
        private string AccessToken;

        public List<ActivityRootObject> ActvityList { get; set; }

        public void Init(string athleteId, string stravaId, string accessToken)
        {
            AthleteId = athleteId;
            StravaId = stravaId;
            AccessToken = accessToken;
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

        public async Task<bool> EstablishConnection()
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(RequestFactory.RequestFactory.GetSingleton().PROJECT_PACKAGE);
            return CheckResponseCode(response);
        }

        public string GetJsonMessage(HttpResponseMessage response)
        {
            return (string)JObject.Parse(new StreamReader(response.Content.ReadAsStreamAsync().Result).ReadToEnd()).SelectToken("message");
        }

        public async Task<bool> Create()
        {
            string test = RequestFactory.RequestFactory.GetSingleton().CREATE_ACTIVITIES + "?athleteId=" + AthleteId + "&stravaId=" + StravaId + "&accessToken=" + AccessToken;

            var client = new HttpClient
            {
                BaseAddress = new Uri(RequestFactory.RequestFactory.GetSingleton().CREATE_ACTIVITIES + "?athleteId="+ AthleteId +"&stravaId="+StravaId+"&accessToken="+AccessToken)
            };
            HttpResponseMessage response = await client.GetAsync("");
            return CheckResponseCode(response);
        }


        public Task<ActivityRootObject> Find(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ActivityRootObject>> FindAll()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(RequestFactory.RequestFactory.GetSingleton().LIST_ACTIVITIES + "?athleteId="+ AthleteId)
            };
            HttpResponseMessage response = await client.GetAsync("");

            return JsonConvert.DeserializeObject<List<ActivityRootObject>>(await response.Content.ReadAsStringAsync());
        }

 
    }
}
