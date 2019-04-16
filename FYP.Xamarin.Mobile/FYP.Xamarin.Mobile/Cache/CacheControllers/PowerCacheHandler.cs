using FYP.Xamarin.Mobile.Cache.CacheControllers;
using FYP.Xamarin.Mobile.Database;
using FYP.Xamarin.Mobile.Database.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FYP.Xamarin.Mobile.ViewModels
{
    public class PowerCacheHandler : ICacheHandlerFacade<Power>
    {
        private CacheManager<Power> power_DbHandler;
        private List<Power> powerCache;
        private long ActivityId;
        private string Stream;

        public PowerCacheHandler()
        {
            powerCache = null;
            power_DbHandler = new CacheManager<Power>();
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
                Power power = new Power(ActivityId, Stream);
                await power_DbHandler.Insert(power);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public Task<Power> Find(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Power>> FindList(long activityId)
        {
            powerCache = await power_DbHandler.Get<Power>();
            return powerCache.FindAll(c => (c.activityId == activityId));
        }


        public async Task<List<Power>> FindAll()
        {
            return await power_DbHandler.Get<Power>();

        }

    }
}


