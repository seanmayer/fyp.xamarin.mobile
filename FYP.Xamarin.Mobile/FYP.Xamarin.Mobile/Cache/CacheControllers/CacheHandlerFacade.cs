using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Xamarin.Mobile.Cache.CacheControllers
{
    public interface ICacheHandlerFacade<T>
    {
        Task<bool> Create();
        Task<T> Find(long id);
        Task<List<T>> FindList(long id);
        Task<List<T>> FindAll();
    }
}
