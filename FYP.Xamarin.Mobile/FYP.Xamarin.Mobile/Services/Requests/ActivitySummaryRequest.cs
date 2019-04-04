using FYP.Xamarin.Mobile.Services.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Services.RequestFactory
{
    public class ActivitySummaryRequest : IRequest<ActivitySummaryRequest>
    {
     
        public string LIST_ACTIVITYSUMMARY = "services.activitysummary/list/activitysummaries";
        public string CREATE_ACTIVITYSUMMARY = "services.activitysummary/create/activitysummaries";

        public string MakeCreateRequest()
        {
            return CREATE_ACTIVITYSUMMARY;
        }

        public string MakeGetRequest()
        {
            throw new NotImplementedException();
        }

        public string MakeListRequest()
        {
            return LIST_ACTIVITYSUMMARY;
        }
    }
}
