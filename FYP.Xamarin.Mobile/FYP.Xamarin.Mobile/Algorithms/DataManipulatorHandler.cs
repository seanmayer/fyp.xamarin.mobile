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

        public decimal GetPercentageDifference(int V1, int V2)
        {
            return ((V2 - V1) / Math.Abs(V1)) * 100;
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

        public async Task<Dictionary<long, int>> GetDailyPeakAverages(string MenuSelection, string month, int Seconds)
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

        public async Task<Dictionary<long, string>> GetDatesNoDuplicates(string month)
        {
            Dictionary<long, string> dates = new Dictionary<long, string>();
            int index = 0;
            foreach (var activity in await activityCacheHandler.FindAll())
            {
                if (!(dates.ContainsValue(FormatterHandler.Instance.ConvertGMTToDDMMYYYY(activity.startDate))) && FormatterHandler.Instance.ConvertGMTToMonth(activity.startDate).Equals(month))
                {
                    dates.Add(index++, FormatterHandler.Instance.ConvertGMTToDDMMYYYY(activity.startDate));
                }
            }
            return dates;
        }

        public async Task<Dictionary<string, int>> GetDailyTopPeakAverages(string node, string month, int seconds)
        {
            Dictionary<string, int> dailyTotals = new Dictionary<string, int>();
            List<Activity> activitiyCache = await activityCacheHandler.FindAll();

            foreach (var date in await GetDatesNoDuplicates(month))
            {
                Dictionary<long, int> temp = new Dictionary<long, int>();
                foreach (var val in await GetDailyPeakAverages(node, month, seconds))
                {
                    if (FormatterHandler.Instance.ConvertGMTToDDMMYYYY(activitiyCache.Find(x => x.activityId == val.Key).startDate).Equals(date.Value))
                    {
                        temp.Add(val.Key, val.Value);
                    }
                }
                dailyTotals.Add(date.Value, temp.Values.Max());
            }
            return dailyTotals;
        }
    }
}
