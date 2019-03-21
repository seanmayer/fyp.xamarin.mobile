using FYP.Xamarin.Mobile.Services.Requests;
using System;

namespace FYP.Xamarin.Mobile.Services.RequestFactory
{
    public class CadenceStreamRequest : IRequest<CadenceStreamRequest>
    {
     
        public string LIST_CADENCESTREAM = "services.cadence/list/cadencestream";
        public string CREATE_CADENCESTREAM = "services.cadence/create/cadencestream/";

        public string MakeCreateRequest()
        {
            return CREATE_CADENCESTREAM;
        }

        public string MakeGetRequest()
        {
            throw new NotImplementedException();
        }

        public string MakeListRequest()
        {
            return LIST_CADENCESTREAM;
        }
    }
}
