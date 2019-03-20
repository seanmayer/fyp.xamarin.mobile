using FYP.Xamarin.Mobile.Database.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Xamarin.Mobile.Database
{
    public class CredentialsCacheHandler
    {
        private CacheManager<Credentials> credentials_DbHandler;
        private long CredId;
        private string Username;
        private string Password;

        public void Init(long credId, string username, string password)
        {
            this.CredId = credId;
            this.Username = username;
            this.Password = password;
        }

        public CredentialsCacheHandler()
        {
            credentials_DbHandler = new CacheManager<Credentials>();
        }

        public async Task<bool> Create()
        {
            try
            {
                Credentials cred = new Credentials(CredId, Username, Password);
                await credentials_DbHandler.Insert(cred);
                return true;
            }
            catch (Exception) { return false; }
        }

        public async Task<Credentials> Find(string username, string password)
        {
            List<Credentials> myList = await credentials_DbHandler.Get<Credentials>();
            return myList.Find(c => (c.Username == username) && (c.Password == password));
        }

        public async Task<List<Credentials>> FindAll()
        {
            return await credentials_DbHandler.Get<Credentials>();

            //foreach (Credentials c in credentials)
            //{
            //    await DisplayAlert("Message", c.Username, "OK");
            //}
        }
    }
}
