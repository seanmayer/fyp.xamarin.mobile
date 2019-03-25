using Microcharts;
using SkiaSharp;
using Microcharts.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Entry = Microcharts.Entry;
using Xamarin.Forms.Xaml;
using FYP.Xamarin.Mobile.Services.Model;
using FYP.Xamarin.Mobile.Services;
using FYP.Xamarin.Mobile.Database;
using FYP.Xamarin.Mobile.ViewModels;
using FYP.Xamarin.Mobile.Database.Model;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using FYP.Xamarin.Mobile.Streams;
using FYP.Xamarin.Mobile.Streams.StreamFactory;

namespace FYP.Xamarin.Mobile.ViewsModel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityAnaylsis : ContentPage
    {
        private Task<Dictionary<int, long>> Stream;
        private List<Entry> Entries = new List<Entry>();

        public ActivityAnaylsis(Activity activity, string accessToken, string menuSelection)
        {
            InitializeComponent();
            Title = menuSelection;
            ApplyStyles();
            Stream = StreamFactory.GetSingleton(activity, accessToken).CreateStream(menuSelection);
            GetPeaksTitle(menuSelection);
            LoadChart(Stream);
            LoadLabels(Stream);
        }

        public void ApplyStyles()
        {
            BackgroundColor = Color.FromHex("#1F2D44");
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#1F2D44");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }

        public void GetPeaksTitle(String menuSelection)
        {
            switch (menuSelection)
            {
                case "Power": PeaksTitle.Text = "Power Peaks (W)";
                    break;
                case "Cadence": PeaksTitle.Text = "Cadence Peaks (RPM)";
                    break;
                case "Speed": PeaksTitle.Text = "Speed Peaks (MPH)";
                    break;
                default: PeaksTitle.Text = "Unavailable";
                    break;
            }
        }

        public async void LoadChart(Task<Dictionary<int, long>> stream)
        {
            foreach (KeyValuePair<int, long> entry in await stream)
            {
                Entries.Add(new Entry(entry.Value)
                {
                    Color = SKColor.Parse("#00CED1"),
                });
            }
            Chart2.Chart = new LineChart()
            {
                Entries = Entries,
                LineMode = LineMode.Straight,
                LineSize = 1,
                PointMode = PointMode.None,
                PointSize = 1,
            };

            MaxLabel.Text = (Stream.Result.Values.Max()).ToString();
        }

        public async void LoadLabels(Task<Dictionary<int, long>> stream)
        {
            TenSecLabel.Text = ((int)GetHighestSequenceXAverage(10, await stream)).ToString();
            TwentySecLabel.Text = ((int)GetHighestSequenceXAverage(20, await stream)).ToString();
            ThirtySecLabel.Text = ((int)GetHighestSequenceXAverage(30, await stream)).ToString();
        }

        public double GetHighestSequenceXAverage(int Seconds, Dictionary<int, long> Stream)
        {
            int Count = 0;
            Dictionary<int, double> Averages = new Dictionary<int, double>();
            for (int i = 0; i < ((Stream.Keys.Max()) / Seconds); i++)
            {
                Dictionary<int, long> XElements = new Dictionary<int, long>();
                for (int s = 0; s < Seconds; s++)
                {
                    int key1 = Stream.ElementAt(Count + s).Key;
                    XElements.Add(Stream.ElementAt(Count + s).Key, Stream.ElementAt(Count + s).Value);
                }
                Averages.Add(i, XElements.Values.Average());
                XElements.Clear();
                Count++;
            }
            return Averages.Values.Max();
        }
    }
}