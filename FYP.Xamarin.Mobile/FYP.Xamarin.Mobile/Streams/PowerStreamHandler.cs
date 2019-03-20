using FYP.Xamarin.Mobile.Database.Model;
using FYP.Xamarin.Mobile.Services;
using FYP.Xamarin.Mobile.Services.Model;
using FYP.Xamarin.Mobile.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Xamarin.Mobile.Streams
{
    public class PowerStreamHandler : IStream<PowerRootObject>
    {
        private long ActivityId;
        private PowerStreamServiceHandler powerStreamServiceHandler;
        private PowerCacheHandler powerCacheHandler;
        public Dictionary<int, long> powerStream;

        public PowerStreamHandler(Activity activity, string accessToken)
        {
            this.ActivityId = activity.activityId;
            powerStreamServiceHandler = new PowerStreamServiceHandler();
            powerCacheHandler = new PowerCacheHandler();
            powerStreamServiceHandler.Init(activity.activityId.ToString(), activity.stravaid.ToString(), accessToken);
        }

        public async Task<List<PowerRootObject>> RequestFindStream()
        {
            try
            {
                List<PowerRootObject> x = await powerStreamServiceHandler.FindAll();
                return x;
            }
            catch (Exception e)
            {
                //await DisplayAlert("Offline", "Unable to reach server" + e, "OK");
                return null;
            }
        }

        public async Task RequestCreateStream()
        {
            try
            {
                await powerStreamServiceHandler.Create();
            }
            catch (Exception e)
            {
                //await DisplayAlert("Error", e.ToString(), "OK");
            }

        }

        public async Task<bool> CheckCache()
        {
            List<Power> list = await powerCacheHandler.Find(ActivityId);
            if (list.Count() != 0)
            {
                //await DisplayAlert("Done", "Exists", "OK");
                return true;
            }
            else
            {
                //await DisplayAlert("Done", "Does not Exists", "OK"); 
                return false;
            }
        }

        public async Task<bool> CacheCreateStream()
        {
            try
            {
                foreach (var power in await RequestFindStream())
                {
                    powerCacheHandler.Init(ActivityId, power.powerstream);
                    await powerCacheHandler.Create();
                }
                //await DisplayAlert("Done", "Created Power Cache", "OK");
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
            foreach (Power p in await powerCacheHandler.Find(ActivityId))
            {
                return JsonConvert.DeserializeObject<Dictionary<int, long>>(p.stream);
            }
            return null;
            //RefreshChart();
        }
    }
}
