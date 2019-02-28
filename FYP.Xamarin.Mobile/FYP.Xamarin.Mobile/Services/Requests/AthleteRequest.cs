using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Services.Requests
{
    public class AthleteRequest  : IRequest
    {
        public string CREATE_ATHLETE = "services.athlete/create/athlete/";
        public string LIST_ATHLETE = "services.athlete/list/athletes/";
        public string GET_ATHLETE = "services.athlete/get/athlete/";

        public string MakeCreateRequest()
        {
            return CREATE_ATHLETE;
        }

        public string MakeGetRequest()
        {
            return GET_ATHLETE;
        }

        public string MakeListRequest()
        {
            return LIST_ATHLETE;
        }
    }
}
