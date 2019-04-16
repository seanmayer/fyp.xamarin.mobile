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
    public class CadenceStreamHandler : IStream<CadenceRootObject>
    {
        private long ActivityId;
        private CadenceStreamServiceHandler cadenceStreamServiceHandler;
        private CadenceCacheHandler cadenceCacheHandler;
        public Dictionary<int, long> cadenceStream;

        public CadenceStreamHandler(Activity activity, string accessToken)
        {
            cadenceStream = null;
            this.ActivityId = activity.activityId;
            cadenceStreamServiceHandler = new CadenceStreamServiceHandler();
            cadenceCacheHandler = new CadenceCacheHandler();
            cadenceStreamServiceHandler.Init(activity.activityId.ToString(), activity.stravaid.ToString(), accessToken);
        }

        public async Task<List<CadenceRootObject>> RequestFindStream()
        {
            try
            {
                List<CadenceRootObject> x = await cadenceStreamServiceHandler.FindAll();
                return x;
            }
            catch (Exception)
            {
                new NullCadenceRootObject();
                return null;
            }
        }

        public async Task RequestCreateStream()
        {
            try
            {
                await cadenceStreamServiceHandler.Create();
            }
            catch (Exception)
            {
                new NullCadenceRootObject();
            }

        }

        public async Task<bool> CheckCache()
        {
            List<Cadence> list = await cadenceCacheHandler.FindList(ActivityId);
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
                foreach (var cadence in await RequestFindStream())
                {
                    cadenceCacheHandler.Init(ActivityId, cadence.cadencestream);
                    await cadenceCacheHandler.Create();
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
            foreach (Cadence c in await cadenceCacheHandler.FindList(ActivityId))
            {
                return JsonConvert.DeserializeObject<Dictionary<int, long>>(c.stream);
            }
            return null;
        }
    }
}
