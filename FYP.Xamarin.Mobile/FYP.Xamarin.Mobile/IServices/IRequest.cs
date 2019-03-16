using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Services.Requests
{
    interface IRequest<T>
    {
        string MakeCreateRequest();
        string MakeGetRequest();
        string MakeListRequest();
    }
}
