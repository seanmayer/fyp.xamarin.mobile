using FYP.Xamarin.Mobile.Algorithms;
using FYP.Xamarin.Mobile.Database;
using FYP.Xamarin.Mobile.Database.Model;
using FYP.Xamarin.Mobile.Errors;
using FYP.Xamarin.Mobile.Formatters;
using FYP.Xamarin.Mobile.Renders;
using FYP.Xamarin.Mobile.Streams.StreamFactory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entry = Microcharts.Entry;
using SkiaSharp;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;

namespace FYP.Xamarin.Mobile.ViewsModel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TrendAnalysis : ContentPage
	{
        private ActivityCacheHandler activityCacheHandler;
        
        private List<Entry> Entries = new List<Entry>();
        private Task<Dictionary<long, int>> TrendData;
        private string MenuSelection;
        private string AccessToken;

        public TrendAnalysis (string athleteId, string stravaId, string accessToken, string menuSelection)
		{
			InitializeComponent ();
            this.MenuSelection = menuSelection;
            this.AccessToken = accessToken;
            activityCacheHandler = new ActivityCacheHandler();
            
            activityCacheHandler.Init(Int64.Parse(athleteId));
            LoadScreen();

        }

        public void LoadScreen()
        {
            this.TrendData = PopulateTrendAnalysis("December");
            LoadChart(TrendData);
        }

        public async Task<Dictionary<long,int>> PopulateTrendAnalysis(string month)
        {
            Dictionary<long, int> TrendData = new Dictionary<long, int>();
            foreach (Activity activity in await activityCacheHandler.FindAll())
            {
                if (FormatterHandler.Instance.ConvertGMTToMonth(activity.startDate).Equals(month))
                {
                    try
                    {
                        TrendData.Add(activity.activityId, Int32.Parse(ErrorHandler.Instance.CheckStreamSequenceNotOutAbounds(((int)DataManipulatorHandler.Instance
                                                       .GetHighestSequenceXAverage(30, await StreamFactory.GetSingleton(activity, AccessToken).CreateStream(MenuSelection))))));
                    }
                    catch (Exception e)
                    {

                    }

                }
            }
            return TrendData;
        }



        public async void LoadChart(Task<Dictionary<long, int>> stream)
        {
            foreach (KeyValuePair<long, int> entry in await stream)
            {
                Entries.Add(new Entry(entry.Value)
                {
                    Color = SkiaSharp.SKColor.Parse(ChartColourHandler.Instance.GetCustomStyles(MenuSelection)),
                    ValueLabel = entry.Value.ToString()
                });
             
            }

            Chart2.Chart = new LineChart()
            {
                Entries = Entries,
                LineMode = LineMode.Straight,
                LineSize = 8,
                PointMode = PointMode.Square,
                PointSize = 18
            };

        }
    }
}