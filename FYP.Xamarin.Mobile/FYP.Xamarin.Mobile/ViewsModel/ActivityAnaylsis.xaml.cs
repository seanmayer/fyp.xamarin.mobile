using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Entry = Microcharts.Entry;
using Xamarin.Forms.Xaml;
using FYP.Xamarin.Mobile.Database.Model;
using FYP.Xamarin.Mobile.Streams.StreamFactory;

namespace FYP.Xamarin.Mobile.ViewsModel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityAnaylsis : ContentPage
    {
        private Task<Dictionary<int, long>> Stream;
        private List<Entry> Entries = new List<Entry>();
        private String ChartColour;
        private Activity Activity;
        private string AccessToken;
        private string MenuSelection;

        public ActivityAnaylsis(Activity activity, string accessToken, string menuSelection)
        {
                InitializeComponent();
                Title = menuSelection;
                ApplyStyles();
                this.Activity = activity;
                this.MenuSelection = menuSelection;
                this.AccessToken = accessToken;
                LoadScreen();

            TenSecLabelTitle.GestureRecognizers.Add(
            new TapGestureRecognizer()
            {
                Command = new Command(() => {
                    Navigation.PushAsync(new Loading());
                })
            }
        );


        }

        public void LoadScreen()
        {
            GetCustomStyles(MenuSelection);
            Stream = StreamFactory.GetSingleton(Activity, AccessToken).CreateStream(MenuSelection);
            LoadChart(Stream);
            LoadLabels(Stream);
        }

            public void ApplyStyles()
        {
            BackgroundColor = Color.FromHex("#1F2D44");
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#1F2D44");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }

        public void GetCustomStyles(String menuSelection)
        {
            switch (menuSelection)
            {
                case "Power": PeaksTitle.Text = "Power Peaks "; UnitsLabel1.Text ="watts"; UnitsLabel2.Text = "watts"; UnitsLabel3.Text = "watts"; UnitsLabel4.Text = "watts";
                    ChartColour = "#EC5D5D";
                    break;
                case "Cadence": PeaksTitle.Text = "Cadence Peaks "; UnitsLabel1.Text = "rpm"; UnitsLabel2.Text = "rpm"; UnitsLabel3.Text = "rpm"; UnitsLabel4.Text = "rpm";
                    ChartColour = "#A5C2A3";
                    break;
                case "Speed": PeaksTitle.Text = "Speed Peaks "; UnitsLabel1.Text = "mph"; UnitsLabel2.Text = "mph"; UnitsLabel3.Text = "mph"; UnitsLabel4.Text = "mph";
                    ChartColour = "#7EBDD1";
                    break;
                default: PeaksTitle.Text = "Unavailable"; UnitsLabel1.Text = "n/a"; UnitsLabel2.Text = "n/a"; UnitsLabel3.Text = "n/a"; UnitsLabel4.Text = "n/a";
                    ChartColour = "#707070";
                    break;
            }
        }

        public async void LoadChart(Task<Dictionary<int, long>> stream)
        {
            foreach (KeyValuePair<int, long> entry in await stream)
            {
                Entries.Add(new Entry(entry.Value)
                {
                    Color = SKColor.Parse(ChartColour),
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

           
        }

        public async void LoadLabels(Task<Dictionary<int, long>> stream)
        {
            Dictionary<int, long> loadStream = await stream;
            if(loadStream.Count != 0)
            { 
                MaxLabel.Text = loadStream.Values.Max().ToString();
                TenSecLabel.Text = ((int)GetHighestSequenceXAverage(10, loadStream)).ToString();
                TwentySecLabel.Text = ((int)GetHighestSequenceXAverage(20, loadStream)).ToString();
                ThirtySecLabel.Text = ((int)GetHighestSequenceXAverage(30, loadStream)).ToString();
            }
            else
            {
                await DisplayAlert("Oops", "No data logged!", "OK");
                await Navigation.PushAsync(new ActivityMenu((Activity)Activity, AccessToken));
            }
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