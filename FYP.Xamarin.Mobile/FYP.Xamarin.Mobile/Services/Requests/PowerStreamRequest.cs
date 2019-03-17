using FYP.Xamarin.Mobile.Services.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Services.RequestFactory
{
    public class PowerStreamRequest : IRequest<PowerStreamRequest>
    {
     
        public string LIST_POWERSTREAM = "services.power/list/powerstream";
        public string CREATE_POWERSTREAM = "services.power/create/powerstream/";

        public string MakeCreateRequest()
        {
            return CREATE_POWERSTREAM;
        }

        public string MakeGetRequest()
        {
            throw new NotImplementedException();
        }

        public string MakeListRequest()
        {
            return LIST_POWERSTREAM;
        }
    }
}
