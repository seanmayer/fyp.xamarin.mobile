using FYP.Xamarin.Mobile.Services.Requests;
using System;

namespace FYP.Xamarin.Mobile.Services.RequestFactory
{
    public class SpeedStreamRequest : IRequest<SpeedStreamRequest>
    {
     
        public string LIST_SPEEDSTREAM = "services.speed/list/speedstream";
        public string CREATE_SPEEDSTREAM = "services.speed/create/speedstream/";

        public string MakeCreateRequest()
        {
            return CREATE_SPEEDSTREAM;
        }

        public string MakeGetRequest()
        {
            throw new NotImplementedException();
        }

        public string MakeListRequest()
        {
            return LIST_SPEEDSTREAM;
        }
    }
}
