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
    public class PowerStreamServiceHandler : IServerServices<PowerRootObject>
    {
        private string ActivityId;
        private string StravaId;
        private string AccessToken;

        public List<PowerRootObject> ActvityList { get; set; }

        public void Init(string activityId, string stravaId, string accessToken)
        {
            ActivityId = activityId;
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
            var client = new HttpClient
            {
                BaseAddress = new Uri(RequestFactory.RequestFactory.GetSingleton().CREATE_POWERSTREAM + "?activityId=" + ActivityId + "&stravaId="+StravaId+"&accessToken="+AccessToken)
            };
            HttpResponseMessage response = await client.GetAsync("");
            return CheckResponseCode(response);
        }


        public Task<PowerRootObject> Find(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PowerRootObject>> FindAll()
        {
            string test = RequestFactory.RequestFactory.GetSingleton().LIST_POWERSTREAM;
            var client = new HttpClient
            {
                BaseAddress = new Uri(RequestFactory.RequestFactory.GetSingleton().LIST_POWERSTREAM + "?activityId=" + ActivityId)
            };
            HttpResponseMessage response = await client.GetAsync("");
            string test2 = (string)JObject.Parse(new StreamReader(response.Content.ReadAsStreamAsync().Result).ReadToEnd()).SelectToken("powerstream");

            List<PowerRootObject> powerstream = new List<PowerRootObject>();
            powerstream.Add(new PowerRootObject((string)JObject.Parse(new StreamReader(response.Content.ReadAsStreamAsync().Result).ReadToEnd()).SelectToken("powerstream")));

            return powerstream;
        }

 
    }
}
