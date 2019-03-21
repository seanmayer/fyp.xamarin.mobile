using FYP.Xamarin.Mobile.Database.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FYP.Xamarin.Mobile.Streams.StreamFactory
{
    public class StreamFactory
    {
        private static StreamFactory Instance;
        private Activity Activity;
        private string AccessToken;

        public StreamFactory(Activity activity, string accessToken)
        {
            this.Activity = activity;
            this.AccessToken = accessToken;
        }

        public static StreamFactory GetSingleton(Activity activity, string accessToken)
        {
            Instance = new StreamFactory(activity, accessToken);
            return Instance;
        }

        public Task<Dictionary<int, long>> CreateStream(string type)
        {
            switch (type)
            {
                case "Power":
                    return new PowerStreamHandler(Activity, AccessToken).SyncCachedStream();
                case "Cadence":
                    return new CadenceStreamHandler(Activity,AccessToken).SyncCachedStream();
                case "Speed":
                    return new SpeedStreamHandler(Activity, AccessToken).SyncCachedStream();
                default:
                    return null;
            }
        }


    }
}
