using FYP.Xamarin.Mobile.Database;
using FYP.Xamarin.Mobile.Database.Model;
using FYP.Xamarin.Mobile.Errors;
using FYP.Xamarin.Mobile.Formatters;
using FYP.Xamarin.Mobile.Streams.StreamFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Xamarin.Mobile.Algorithms
{
    public class DataManipulatorHandler
    {

        private static volatile DataManipulatorHandler singletonInstance;
        private ActivityCacheHandler activityCacheHandler;
        private string AccessToken;

        private DataManipulatorHandler()
        {

        }

        private DataManipulatorHandler(string athleteId, string accessToken)
        {
            this.AccessToken = accessToken;
            activityCacheHandler = new ActivityCacheHandler();
            activityCacheHandler.Init(Int64.Parse(athleteId));
        }

        public static DataManipulatorHandler CreateSingleton()
        {
            if (singletonInstance == null)
            {
                singletonInstance = new DataManipulatorHandler();
            }

            return singletonInstance;
        }

        public static DataManipulatorHandler CreateSingleton(string athleteId, string accessToken)
        {
            if (singletonInstance == null)
            {
                singletonInstance = new DataManipulatorHandler(athleteId, accessToken);
            }

            return singletonInstance;
        }

        public static DataManipulatorHandler Instance
        {
            get { return singletonInstance; }
        }


        public async Task<Dictionary<long, int>> PeakMax(string MenuSelection, string month)
        {
            Dictionary<long, int> TrendData = new Dictionary<long, int>();
            foreach (Activity activity in await activityCacheHandler.FindAll())
            {
                if (FormatterHandler.Instance.ConvertGMTToMonth(activity.startDate).Equals(month))
                {
                    try
                    {
                        TrendData.Add(activity.activityId, await GetPeakMaxElementToAddAsync(activity, MenuSelection));
                    }
                    catch (Exception) { }
                }
                else if (month.Equals("all"))
                {
                    try
                    {
                        TrendData.Add(activity.activityId, await GetPeakMaxElementToAddAsync(activity, MenuSelection));
                    }
                    catch (Exception) { }
                }
            }
            return TrendData;
        }

        public async Task<Dictionary<long, int>> PeakAverage(string MenuSelection, string month, int Seconds)
        {
            Dictionary<long, int> TrendData = new Dictionary<long, int>();
            foreach (Activity activity in await activityCacheHandler.FindAll())
            {
                if (FormatterHandler.Instance.ConvertGMTToMonth(activity.startDate).Equals(month))
                {
                    try
                    {
                        TrendData.Add(activity.activityId,await GetPeakAverageElementToAddAsync(Seconds, activity,MenuSelection));
                    }
                    catch (Exception) { }
                }
                else if(month.Equals("all"))
                {
                    try
                    {
                        TrendData.Add(activity.activityId, await GetPeakAverageElementToAddAsync(Seconds, activity, MenuSelection));
                    }
                    catch (Exception) { }
                }
            }
            return TrendData;
        }

        public async Task<int> GetPeakMaxElementToAddAsync(Activity activity, string MenuSelection)
        {
            return Int32.Parse(ErrorHandler.Instance.CheckStreamSequenceNotOutAbounds(((int)
                               GetHighestMax(await
                               StreamFactory.GetSingleton(activity, AccessToken)
                               .CreateStream(MenuSelection)))));
        }


        public async Task<int> GetPeakAverageElementToAddAsync(int Seconds, Activity activity, string MenuSelection)
        {
            return Int32.Parse(ErrorHandler.Instance.CheckStreamSequenceNotOutAbounds(((int)
                               GetHighestSequenceXAverage(Seconds, await
                               StreamFactory.GetSingleton(activity, AccessToken)
                               .CreateStream(MenuSelection)))));
        }

        public double GetHighestMax(Dictionary<int, long> Stream)
        {
            try
            {
                return Stream.Values.Max();

            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public double GetHighestSequenceXAverage(int Seconds, Dictionary<int, long> Stream)
        {
            try
            {
                Dictionary<int, double> Averages = new Dictionary<int, double>();

                decimal numberOfGroups = Stream.Keys.Max() / Seconds;
                int counter = 0;
                int groupSize = Convert.ToInt32(Math.Ceiling(Stream.Count / numberOfGroups));

                IEnumerable<IGrouping<int, KeyValuePair<int, long>>> groupedData = Stream.GroupBy(x => counter++ / groupSize);

                foreach (var group in groupedData)
                {
                    Averages.Add(group.Key, group.Average(t => t.Value));
                }

                return Averages.Values.Max();

            }
            catch (Exception e)
            {
                return -1;
            }
        }
    }
}
