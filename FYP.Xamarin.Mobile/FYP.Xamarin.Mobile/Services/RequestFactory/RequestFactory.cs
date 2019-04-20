using FYP.Xamarin.Mobile.Services.Requests;


namespace FYP.Xamarin.Mobile.Services.RequestFactory
{ 

    public class RequestFactory
    {
        private static RequestFactory Instance = new RequestFactory();

        private readonly int SERVER_PORT = 8080;
        private readonly string SERVER_IP  = "10.0.2.2";
        private readonly string _PROJECT_PACKAGE = "FYP.SCPSAA.Web.Services/";
        private readonly string _SERVICE_PACKAGE = "webresources/";

        private IRequest<AthleteRequest> AthleteRequest = new AthleteRequest();
        private IRequest<CredentialsRequest> CredentialRequest = new CredentialsRequest();
        private IRequest<ActivityRequest> ActivityRequest = new ActivityRequest();
        private IRequest<PowerStreamRequest> PowerStreamRequest = new PowerStreamRequest();
        private IRequest<CadenceStreamRequest> CadenceStreamRequest = new CadenceStreamRequest();
        private IRequest<SpeedStreamRequest> SpeedStreamRequest = new SpeedStreamRequest();
        private IRequest<ActivitySummaryRequest> ActivitySummaryRequestRequest = new ActivitySummaryRequest();

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
                get => SERVICE_PACKAGE + ActivityRequest.MakeCreateRequest();
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
        public string CREATE_SPEEDSTREAM
        {
            get => SERVICE_PACKAGE + SpeedStreamRequest.MakeCreateRequest();
        }
        public string LIST_SPEEDSTREAM
        {
            get => SERVICE_PACKAGE + SpeedStreamRequest.MakeListRequest();
        }
        public string CREATE_CADENCESTREAM
        {
            get => SERVICE_PACKAGE + CadenceStreamRequest.MakeCreateRequest();
        }
        public string LIST_CADENCESTREAM
        {
            get => SERVICE_PACKAGE + CadenceStreamRequest.MakeListRequest();
        }

        public string CREATE_ACTIVITYSUMMARY
        {
            get => SERVICE_PACKAGE + ActivitySummaryRequestRequest.MakeCreateRequest();
        }
        public string LIST_ACTIVITYSUMMARY
        {
            get => SERVICE_PACKAGE + ActivitySummaryRequestRequest.MakeListRequest();
        }

    }
}
