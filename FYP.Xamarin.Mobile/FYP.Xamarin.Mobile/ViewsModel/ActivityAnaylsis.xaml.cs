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
using System.Collections.Concurrent;
using FYP.Xamarin.Mobile.Renders;
using FYP.Xamarin.Mobile.Errors;
using FYP.Xamarin.Mobile.Algorithms;
using FYP.Xamarin.Mobile.Formatters;

namespace FYP.Xamarin.Mobile.ViewsModel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityAnaylsis : ContentPage
    {
        private Task<Dictionary<int, long>> Stream;
        private List<Entry> Entries = new List<Entry>();
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
            TenSecLabelTitle.GestureRecognizers.Add(new TapGestureRecognizer(){Command = new Command(() =>{Navigation.PushAsync(new Loading());})});
        }

        public void LoadScreen()
        {
            PeaksTitle.Text = LabelHandler.Instance.GetPeaksTitle(MenuSelection);
            UnitsLabel1.Text = LabelHandler.Instance.GetPeaksLabel(MenuSelection);
            UnitsLabel2.Text = LabelHandler.Instance.GetPeaksLabel(MenuSelection);
            UnitsLabel3.Text = LabelHandler.Instance.GetPeaksLabel(MenuSelection);
            UnitsLabel4.Text = LabelHandler.Instance.GetPeaksLabel(MenuSelection);
            DataManipulatorHandler.CreateSingleton();
            Stream = StreamFactory.GetSingleton(Activity, AccessToken).CreateStream(MenuSelection);
            DateLabel.Text = FormatterHandler.Instance.ConvertGMTToDDMMYYYY(Activity.startDate);
            LoadChart(Stream);
            LoadLabels(Stream);

        }

        public void ApplyStyles()
        {
            BackgroundColor = Color.FromHex("#1F2D44");
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#1F2D44");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }

        public async void LoadChart(Task<Dictionary<int, long>> stream)
        {
            foreach (KeyValuePair<int, long> entry in await stream)
            {
                Entries.Add(new Entry(entry.Value)
                {
                    Color = ChartColourHandler.Instance.GetSKColorCustomStyles(MenuSelection),
                });

            }
            Chart2.Chart = new LineChart() { Entries = Entries, LineMode = LineMode.Straight, LineSize = 1, PointMode = PointMode.None, PointSize = 1};
        }

        public async void LoadLabels(Task<Dictionary<int, long>> stream)
        {
            Dictionary<int, long> loadStream = await stream;
            if (loadStream.Count != 0)
            {
                MaxLabel.Text = loadStream.Values.Max().ToString();
                TenSecLabel.Text = ErrorHandler.Instance.CheckStreamSequenceNotOutAbounds(
                    ((int)DataManipulatorHandler.Instance.GetHighestSequenceXAverage(10, await stream)));
                TwentySecLabel.Text = ErrorHandler.Instance.CheckStreamSequenceNotOutAbounds(
                    ((int)DataManipulatorHandler.Instance.GetHighestSequenceXAverage(20, await stream)));
                ThirtySecLabel.Text = ErrorHandler.Instance.CheckStreamSequenceNotOutAbounds(
                    ((int)DataManipulatorHandler.Instance.GetHighestSequenceXAverage(30, await stream)));
            }
            else
            {
                await DisplayAlert("Oops", "No data logged!", "OK");
                await Navigation.PushAsync(new ActivityMenu((Activity)Activity, AccessToken));
            }
        }
    }
}