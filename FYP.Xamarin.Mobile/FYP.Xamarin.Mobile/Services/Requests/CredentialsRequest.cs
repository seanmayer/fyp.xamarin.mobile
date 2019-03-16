using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Services.Requests
{
    public class CredentialsRequest : IRequest<CredentialsRequest>
    {
        public string CREATE_CREDENTIALS = "services.credentials/create/credentials/";
        public string LIST_CREDENTIALS = "services.credentials/list/credentials/";

        public string MakeCreateRequest()
        {
            return CREATE_CREDENTIALS;
        }

        public string MakeGetRequest()
        {
            throw new NotImplementedException();
        }

        public string MakeListRequest()
        {
            return LIST_CREDENTIALS;
        }
    }
}
