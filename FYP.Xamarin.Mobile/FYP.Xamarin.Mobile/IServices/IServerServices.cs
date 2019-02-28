using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Xamarin.Mobile.IServices
{
    interface IServerServices<T>
    {
        Task<bool> EstablishConnection();
        bool CheckResponseCode(HttpResponseMessage response);
        string GetJsonMessage(HttpResponseMessage response);
        Task<bool> Create();
        Task<T> Find(string id);
        Task<List<T>> FindAll();
    }
}
