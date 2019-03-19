using FYP.Xamarin.Mobile.Database;
using FYP.Xamarin.Mobile.Database.Model;
using FYP.Xamarin.Mobile.Database.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Xamarin.Mobile.ViewModels
{
    public class AthleteCacheHandler
    {
        private CacheManager<Athlete> athlete_DbHandler;

        private long AthletePK;
        private long CredId;
        private string StravaId;
        private string FirstName;
        private string LastName;
        private string AccessToken;

        public AthleteCacheHandler()
        {
            athlete_DbHandler = new CacheManager<Athlete>();
        }

        public void Init(long athletePK, long credId, string firstName, string lastName, string stravaId, string accessToken)
        {
            this.AthletePK = athletePK;
            this.CredId = credId;
            this.StravaId = stravaId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.AccessToken = accessToken;
        }

        public long CreatNewPK()
        {
            return DateTime.Now.Ticks;
        }

        public async Task<bool> Create()
        {
            try
            {
                Athlete athlete = new Athlete(AthletePK, CredId, StravaId, AccessToken, FirstName, LastName);
                await athlete_DbHandler.Insert(athlete);
                return true;
            }
            catch (Exception) { return false; }
        }

        public async Task<Athlete> Find(long credentialId)
        {
            List<Athlete> myList = await athlete_DbHandler.Get<Athlete>();
            return myList.Find(c => (c.CredentialsId == credentialId));
        }
    }
}
