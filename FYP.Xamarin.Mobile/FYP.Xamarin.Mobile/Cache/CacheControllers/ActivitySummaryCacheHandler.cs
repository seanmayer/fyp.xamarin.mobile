using FYP.Xamarin.Mobile.Cache.CacheControllers;
using FYP.Xamarin.Mobile.Database.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FYP.Xamarin.Mobile.Database
{
    public class ActivitySummaryCacheHandler : ICacheHandlerFacade<ActivitySummary>
    {
        private CacheManager<ActivitySummary> activitySummaryDbHandler;

        private long ActivitySummaryId;
        private string MovingTime;
        private string Distance;
        private string MaxSpeed;
        private string MaxWatts;
        private string AverageSpeed;
        private string AverageWatts;
        private string AverageCadence;
        private string Kilojoules;
        private long ActivityId;

        public ActivitySummaryCacheHandler()
        {
            activitySummaryDbHandler = new CacheManager<ActivitySummary>();
        }

        public void Init(long activitySummaryId, string movingTime, string distance, string maxSpeed, string maxWatts, string averageSpeed, string averageWatts, string averageCadence, string kilojoules,long activityId)
        {
            ActivitySummaryId = activitySummaryId;
            MovingTime = movingTime;
            Distance = distance;
            MaxSpeed = maxSpeed;
            MaxWatts = maxWatts;
            AverageSpeed = averageSpeed;
            AverageWatts = averageWatts;
            AverageCadence = averageCadence;
            Kilojoules = kilojoules;
            ActivityId = activityId;
        }

        public async Task<bool> Create()
        {
            try
            {
                ActivitySummary activity = new ActivitySummary(ActivitySummaryId, MovingTime, Distance, MaxSpeed, MaxWatts, AverageSpeed, AverageWatts, AverageCadence, Kilojoules, ActivityId);
                await activitySummaryDbHandler.Insert(activity);
                return true;
            }
            catch (Exception) { return false; }
        }

        public async Task<ActivitySummary> Find(long id)
        {
            List<ActivitySummary> myList = await activitySummaryDbHandler.Get<ActivitySummary>();
            return myList.Find(c => (c.activitySummaryId == id));
        }

        public Task<List<ActivitySummary>> FindList(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ActivitySummary>> FindAll()
        {
            List<ActivitySummary> myList = await activitySummaryDbHandler.Get<ActivitySummary>();
            myList.Reverse();
            return myList;
        }

    }
}
