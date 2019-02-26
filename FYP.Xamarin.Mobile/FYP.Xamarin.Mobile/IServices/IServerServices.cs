using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FYP.Xamarin.Mobile.IServices
{
    interface IServerServices
    {
        void EstablishConnectionAsync();
        void CheckResponseCode(HttpResponseMessage response);
        string GetJsonMessage(HttpResponseMessage response);
    }
}
