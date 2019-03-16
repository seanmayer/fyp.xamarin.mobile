using FYP.Xamarin.Mobile.Services.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Services.RequestFactory
{
    public class ActivityRequest : IRequest<ActivityRequest>
    {
     
        public string LIST_ACTIVITY = "services.activity/list/activities";
        public string CREATE_ACTIVITY = "services.activity/create/activities/";

        public string MakeCreateRequest()
        {
            return CREATE_ACTIVITY;
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
