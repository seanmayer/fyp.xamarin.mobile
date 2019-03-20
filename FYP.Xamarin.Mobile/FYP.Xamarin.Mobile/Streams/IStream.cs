using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Xamarin.Mobile.Streams
{
    public interface IStream<T>
    {
        Task<List<T>> RequestFindStream();
        Task RequestCreateStream();
        Task<bool> CheckCache();
        Task<bool> CacheCreateStream();
        Task<Dictionary<int, long>> SyncCachedStream();
        Task<Dictionary<int, long>> SetCache();
    }
}

