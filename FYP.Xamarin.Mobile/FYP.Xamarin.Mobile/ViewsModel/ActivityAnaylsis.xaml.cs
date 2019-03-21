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
        private List<Entry> Entries = new List<Entry>();

        public ActivityAnaylsis(Activity activity, string accessToken, string menuSelection)
        {
            InitializeComponent();
            Title = menuSelection;
            ApplyStyles();
            LoadChart(StreamFactory.GetSingleton(activity, accessToken).CreateStream(menuSelection));
        }

        public void ApplyStyles()
        {
            BackgroundColor = Color.FromHex("#1F2D44");
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#1F2D44");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }

        public async void LoadChart(Task<Dictionary<int, long>> entries)
        {
            foreach (KeyValuePair<int, long> entry in await entries)
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
        }
    }
}