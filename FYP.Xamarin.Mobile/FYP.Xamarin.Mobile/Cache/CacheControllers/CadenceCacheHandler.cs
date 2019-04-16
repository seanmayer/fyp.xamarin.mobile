using FYP.Xamarin.Mobile.Cache.CacheControllers;
using FYP.Xamarin.Mobile.Database;
using FYP.Xamarin.Mobile.Database.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FYP.Xamarin.Mobile.ViewModels
{
    public class CadenceCacheHandler : ICacheHandlerFacade<Cadence>
    {
        private CacheManager<Cadence> cadence_DbHandler;
        private List<Cadence> cadenceCache;
        private long ActivityId;
        private string Stream;

        public CadenceCacheHandler()
        {
            cadenceCache = null;
            cadence_DbHandler = new CacheManager<Cadence>();
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
                Cadence cadence = new Cadence(ActivityId, Stream);
                await cadence_DbHandler.Insert(cadence);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Task<Cadence> Find(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Cadence>> FindList(long activityId)
        {
            cadenceCache = await cadence_DbHandler.Get<Cadence>();
            return cadenceCache.FindAll(c => (c.activityId == activityId));
        }

        public async Task<List<Cadence>> FindAll()
        {
            return await cadence_DbHandler.Get<Cadence>();

        }

    }
}


