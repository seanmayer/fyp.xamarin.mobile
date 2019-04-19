using FYP.Xamarin.Mobile.Algorithms;
using FYP.Xamarin.Mobile.Database;
using FYP.Xamarin.Mobile.Database.Model;
using FYP.Xamarin.Mobile.Errors;
using FYP.Xamarin.Mobile.Formatters;
using FYP.Xamarin.Mobile.Renders;
using FYP.Xamarin.Mobile.Streams.StreamFactory;
using System;
using System.Collections.Generic;
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

        public async void LoadScreen()
        {
            Title = MenuSelection;
            MonthTitle1.TextColor = ChartColourHandler.Instance.GetColorCustomStyles(MenuSelection);
            MonthTitle2.TextColor = ChartColourHandler.Instance.GetColorCustomStyles(MenuSelection);
            Highlight1.BackgroundColor = ChartColourHandler.Instance.GetColorCustomStyles(MenuSelection);
            Highlight2.BackgroundColor = ChartColourHandler.Instance.GetColorCustomStyles(MenuSelection);
            frame.IsVisible = true;

            Chart3.Chart = new LineChart()
            {
                Entries = await LoadChart(PopulateTrendAnalysis("December")),
                LineMode = LineMode.Straight,
                LineSize = 8,
                PointMode = PointMode.Square,
                PointSize = 18,
                BackgroundColor = SKColors.White
            };
            Chart3.IsVisible = true;
            

            Chart4.Chart = new LineChart()
            {
                Entries = await LoadChart(PopulateTrendAnalysis("November")),
                LineMode = LineMode.Straight,
                LineSize = 8,
                PointMode = PointMode.Square,
                PointSize = 18,
                BackgroundColor = SKColors.White
            };
            Chart4.IsVisible = true;
            
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
                        TrendData.Add(activity.activityId, 
                                      Int32.Parse(
                                      ErrorHandler.Instance.CheckStreamSequenceNotOutAbounds(((int)
                                      DataManipulatorHandler.Instance.GetHighestSequenceXAverage(30, await 
                                      StreamFactory.GetSingleton(activity, AccessToken)
                                      .CreateStream(MenuSelection))))));
                    }
                    catch (Exception){}
                }
            }
            return TrendData;
        }

        public async Task<List<Entry>> LoadChart(Task<Dictionary<long, int>> stream)
        {
            List<Entry> Entries = new List<Entry>();
            foreach (KeyValuePair<long, int> entry in await stream)
            {
                Entries.Add(new Entry(entry.Value)
                {
                    Color = ChartColourHandler.Instance.GetSKColorCustomStyles(MenuSelection),
                    Label = entry.Value.ToString(),
                    TextColor = ChartColourHandler.Instance.GetSKColorCustomStyles(MenuSelection),
                    ValueLabel = entry.Value.ToString()
                });
            }
            return Entries;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (frame.IsVisible)
            {
                frame.IsVisible = false;
            }
        }
    }
}