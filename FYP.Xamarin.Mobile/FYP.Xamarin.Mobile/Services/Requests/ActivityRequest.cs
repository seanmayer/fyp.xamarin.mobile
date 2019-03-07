using FYP.Xamarin.Mobile.Services.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Services.RequestFactory
{
    public class ActivityRequest : IRequest
    {
     
        public string LIST_ACTIVITY = "services.activity/list/activities";


        public string MakeCreateRequest()
        {
            throw new NotImplementedException();
        }

        public string MakeGetRequest()
        {
            throw new NotImplementedException();
        }

        public string MakeListRequest()
        {
            return LIST_ACTIVITY;
        }
    }
}
