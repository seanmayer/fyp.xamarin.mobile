using FYP.Xamarin.Mobile.Database.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Xamarin.Mobile.Database
{
    public class ActivityCacheHandler
    {
        private CacheManager<Activity> activityDbHandler;

        private long ActivityId;
        private long AthleteId;
        private string Stravaid;
        private string Name;
        private string StartDate;
        private string TimeZone;

        public ActivityCacheHandler()
        {
            activityDbHandler = new CacheManager<Activity>();
        }

        public void Init(long athleteId)
        {
            AthleteId = athleteId;
        }

        public void Init(long activityId, long athleteId, string stravaid, string name, string startDate, string timeZone)
        {
            ActivityId = activityId;
            AthleteId = athleteId;
            Stravaid = stravaid;
            Name = name;
            StartDate = startDate;
            TimeZone = timeZone;
        }

        public long CreatNewPK()
        {
            return DateTime.Now.Ticks;
        }

        public async Task<bool> Create()
        {
            try
            {
                Activity activity = new Activity(ActivityId, AthleteId, Stravaid, Name, StartDate, TimeZone);
                await activityDbHandler.Insert(activity);
                return true;
            }
            catch (Exception) { return false; }
        }
     
        public async Task<List<Activity>> FindAll()
        {
            List<Activity> myList = await activityDbHandler.Get<Activity>();
            return myList.FindAll(p => p.athleteId == AthleteId);
        }


    }
}
