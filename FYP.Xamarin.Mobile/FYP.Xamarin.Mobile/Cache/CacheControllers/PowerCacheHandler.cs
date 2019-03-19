using FYP.Xamarin.Mobile.Database;
using FYP.Xamarin.Mobile.Database.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FYP.Xamarin.Mobile.ViewModels
{
    public class PowerCacheHandler
    {
        private CacheManager<Power> power_DbHandler;

        private long ActivityId;
        private string Stream;

        public PowerCacheHandler()
        {
            power_DbHandler = new CacheManager<Power>();
        }

        public void Init(long activityId, string stream)
        {
            this.ActivityId = activityId;
            this.Stream = stream;
        }

        public long CreatNewPK()
        {
            return DateTime.Now.Ticks;
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

        public async Task<List<Power>> Find(long activityId)
        {
            List<Power> myList = await power_DbHandler.Get<Power>();
            return myList.FindAll(c => (c.activityId == activityId));
        }

        public async Task<List<Power>> FindAll()
        {
            return await power_DbHandler.Get<Power>();

        }
    }
}
