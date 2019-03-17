using FYP.Xamarin.Mobile.Services.Requests;


namespace FYP.Xamarin.Mobile.Services.RequestFactory
{ 

    public class RequestFactory
    {
        private static RequestFactory Instance = new RequestFactory();

        private readonly int SERVER_PORT = 8080;
        private readonly string SERVER_IP  = "192.168.0.130";
        private readonly string _PROJECT_PACKAGE = "FYP.SCPSAA.Web.Services/";
        private readonly string _SERVICE_PACKAGE = "webresources/";

        private IRequest<AthleteRequest> AthleteRequest = new AthleteRequest();
        private IRequest<CredentialsRequest> CredentialRequest = new CredentialsRequest();
        private IRequest<ActivityRequest> ActivityRequest = new ActivityRequest();
        private IRequest<PowerStreamRequest> PowerStreamRequest = new PowerStreamRequest();

        public static RequestFactory GetSingleton()
        {
            return Instance;      
        }
        public string SERVER_ADDRESS
        {
            get => "http://" + SERVER_IP + ":" + SERVER_PORT + "/";
        }
        public string PROJECT_PACKAGE
        {
            get => SERVER_ADDRESS + _PROJECT_PACKAGE;
        }
        public string SERVICE_PACKAGE
        {
            get => PROJECT_PACKAGE + _SERVICE_PACKAGE;
        }
        public string CREATE_ATHLETE
        {
            get => SERVICE_PACKAGE + AthleteRequest.MakeCreateRequest();
        }
        public string FIND_ATHLETE
        {
            get => SERVICE_PACKAGE + AthleteRequest.MakeGetRequest();
        }
        public string LIST_ATHLETE
        {
            get => SERVICE_PACKAGE + AthleteRequest.MakeListRequest();
        }
        public string CREATE_CREDENTIALS
        {
            get => SERVICE_PACKAGE + CredentialRequest.MakeCreateRequest();
        }
        public string LIST_CREDENTIALS
        {
            get => SERVICE_PACKAGE + CredentialRequest.MakeListRequest();
        }
        public string CREATE_ACTIVITIES
        {
                get => SERVICE_PACKAGE + CredentialRequest.MakeCreateRequest();
        }
        public string LIST_ACTIVITIES
        {
            get => SERVICE_PACKAGE + ActivityRequest.MakeListRequest();
        }
        public string CREATE_POWERSTREAM
        {
            get => SERVICE_PACKAGE + PowerStreamRequest.MakeCreateRequest();
        }
        public string LIST_POWERSTREAM
        {
            get => SERVICE_PACKAGE + PowerStreamRequest.MakeListRequest();
        }
    }
}
