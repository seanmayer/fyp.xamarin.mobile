using FYP.Xamarin.Mobile.IServices;
using FYP.Xamarin.Mobile.Services.Model;
using FYP.Xamarin.Mobile.Services.RequestFactory;
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

        public List<ActivityRootObject> ActvityList { get; set; }

        public void Init(string athleteId)
        {
            AthleteId = athleteId;
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

        public Task<bool> Create()
        {
            throw new NotImplementedException();
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

        public Task<ActivityRootObject> Find(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ActivityRootObject>> FindAll()
        {
            string debug = RequestFactory.RequestFactory.GetSingleton().LIST_ACTIVITIES + "?athleteId=" + AthleteId;

            var client = new HttpClient
            {
                BaseAddress = new Uri(RequestFactory.RequestFactory.GetSingleton().LIST_ACTIVITIES + "?athleteId="+ AthleteId)
            };
            HttpResponseMessage response = await client.GetAsync("");

            return JsonConvert.DeserializeObject<List<ActivityRootObject>>(await response.Content.ReadAsStringAsync());
        }

 
    }
}
