using FYP.Xamarin.Mobile.Cache.CacheControllers;
using FYP.Xamarin.Mobile.Database;
using FYP.Xamarin.Mobile.Database.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FYP.Xamarin.Mobile.ViewModels
{
    public class SpeedCacheHandler : ICacheHandlerFacade<Speed>
    {
        private CacheManager<Speed> speed_DbHandler;
        private List<Speed> speedCache;
        private long ActivityId;
        private string Stream;

        public SpeedCacheHandler()
        {
            speedCache = null;
            speed_DbHandler = new CacheManager<Speed>();
        }

        public void Init(long activityId, string stream)
        {
            this.ActivityId = activityId;
            this.Stream = stream;
        }

        public async Task<bool> Create()
        {
            try
            {
                Speed speed = new Speed(ActivityId, Stream);
                await speed_DbHandler.Insert(speed);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Task<Speed> Find(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Speed>> FindList(long activityId)
        {
            speedCache = await speed_DbHandler.Get<Speed>();
            return speedCache.FindAll(c => (c.activityId == activityId));
        }

        public async Task<List<Speed>> FindAll()
        {
            return await speed_DbHandler.Get<Speed>();

        }

    }
}


