using FYP.Xamarin.Mobile.Database.Model;
using FYP.Xamarin.Mobile.Services;
using FYP.Xamarin.Mobile.Services.Model;
using FYP.Xamarin.Mobile.Services.Model.Null_Object;
using FYP.Xamarin.Mobile.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FYP.Xamarin.Mobile.Streams
{
    public class SpeedStreamHandler : IStream<SpeedRootObject>
    {
        private long ActivityId;
        private SpeedStreamServiceHandler speedStreamServiceHandler;
        private SpeedCacheHandler speedCacheHandler;
        public Dictionary<int, long> speedStream;

        public SpeedStreamHandler(Activity activity, string accessToken)
        {
            this.ActivityId = activity.activityId;
            speedStreamServiceHandler = new SpeedStreamServiceHandler();
            speedCacheHandler = new SpeedCacheHandler();
            speedStreamServiceHandler.Init(activity.activityId.ToString(), activity.stravaid.ToString(), accessToken);
        }

        public async Task<List<SpeedRootObject>> RequestFindStream()
        {
            try
            {
                List<SpeedRootObject> x = await speedStreamServiceHandler.FindAll();
                return x;
            }
            catch (Exception)
            {
                new NullSpeedRootObject();
                return null;
            }
        }

        public async Task RequestCreateStream()
        {
            try
            {
                await speedStreamServiceHandler.Create();
            }
            catch (Exception)
            {
                new NullSpeedRootObject();
            }

        }

        public async Task<bool> CheckCache()
        {
            List<Speed> list = await speedCacheHandler.Find(ActivityId);
            if (list.Count() != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CacheCreateStream()
        {
            try
            {
                foreach (var speed in await RequestFindStream())
                {
                    speedCacheHandler.Init(ActivityId, speed.speedstream);
                    await speedCacheHandler.Create();
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<Dictionary<int, long>> SyncCachedStream()
        {
            if (await CheckCache() == false)
            {
                await RequestCreateStream();
                await CacheCreateStream();
            }
            return await SetCache();
        }

        public async Task<Dictionary<int, long>> SetCache()
        {
            foreach (Speed s in await speedCacheHandler.Find(ActivityId))
            {
                return JsonConvert.DeserializeObject<Dictionary<int, long>>(s.stream);
            }
            return null;
        }
    }
}
