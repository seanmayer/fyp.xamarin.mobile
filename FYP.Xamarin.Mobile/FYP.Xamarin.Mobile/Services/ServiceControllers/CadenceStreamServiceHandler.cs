using FYP.Xamarin.Mobile.IServices;
using FYP.Xamarin.Mobile.Services.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace FYP.Xamarin.Mobile.Services
{
    public class CadenceStreamServiceHandler : IServerServices<CadenceRootObject>
    {
        private string ActivityId;
        private string StravaId;
        private string AccessToken;

        public List<CadenceRootObject> CadenceStreamList { get; set; }

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
                BaseAddress = new Uri(RequestFactory.RequestFactory.GetSingleton().CREATE_CADENCESTREAM + "?activityId=" + ActivityId + "&stravaId="+StravaId+"&accessToken="+AccessToken)
            };
            HttpResponseMessage response = await client.GetAsync("");
            return CheckResponseCode(response);
        }


        public Task<CadenceRootObject> Find(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CadenceRootObject>> FindAll()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(RequestFactory.RequestFactory.GetSingleton().LIST_CADENCESTREAM + "?activityId=" + ActivityId)
            };
            HttpResponseMessage response = await client.GetAsync("");
            List<CadenceRootObject> cadencestream = new List<CadenceRootObject>();
            cadencestream.Add(new CadenceRootObject((string)JObject.Parse(new StreamReader(response.Content.ReadAsStreamAsync().Result).ReadToEnd()).SelectToken("cadencestream")));
            return cadencestream;
        }

 
    }
}
