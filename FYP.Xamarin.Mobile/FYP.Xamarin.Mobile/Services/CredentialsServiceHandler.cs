using FYP.Xamarin.Mobile.IServices;
using FYP.Xamarin.Mobile.Json;
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
    public class CredentialsServiceHandler : IServerServices<CredentialsRootObject>
    {
        private string CredId;
        private string Username;
        private string Password;
        public List<CredentialsRootObject> CredentialList { get; set; }

        public void Init(string credId, string username, string password)
        {
            this.CredId = credId;
            this.Username = username;
            this.Password = password;
        }

        public async Task<bool> EstablishConnection()
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(RequestFactory.GetSingleton().PROJECT_PACKAGE);
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
            JObject jsonObject = new JObject(new JProperty("credentialsId", CredId), new JProperty("username", Username), new JProperty("password", Password));

            var content = new StringContent(JsonConvert.SerializeObject(jsonObject), System.Text.Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(new Uri(RequestFactory.GetSingleton().CREATE_CREDENTIALS), content);
            string responseBody = await response.Content.ReadAsStringAsync();

            return CheckResponseCode(response);
        }

        public Task<CredentialsRootObject> Find(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CredentialsRootObject>> FindAll()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(RequestFactory.GetSingleton().LIST_CREDENTIALS)
            };
            HttpResponseMessage response = await client.GetAsync("");

            return JsonConvert.DeserializeObject<List<CredentialsRootObject>>(await response.Content.ReadAsStringAsync());
        }

        public string GetNewId()
        {
            return (CredentialList.OrderByDescending(cred => cred.credentialsId).FirstOrDefault().credentialsId + 1).ToString();
        }

        public bool CheckCredentialsUsernameExists(string username)
        {
            bool exists = CredentialList.Any(cred => cred.username == username);
            if (exists == true)
            {
                return exists;
            }
            else
            {
                return exists;
            }
        }


    }
}
