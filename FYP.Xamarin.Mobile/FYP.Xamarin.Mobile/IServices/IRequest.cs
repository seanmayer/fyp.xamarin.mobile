using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Services.Requests
{
    interface IRequest
    {
        string MakeCreateRequest();
        string MakeListRequest();
    }
}
