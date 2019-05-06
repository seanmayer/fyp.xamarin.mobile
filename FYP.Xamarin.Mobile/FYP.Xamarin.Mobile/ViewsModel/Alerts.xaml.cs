using FYP.Xamarin.Mobile.Algorithms;
using FYP.Xamarin.Mobile.Database;
using Microcharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Entry = Microcharts.Entry;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using FYP.Xamarin.Mobile.Prescriptive;
using static FYP.Xamarin.Mobile.Prescriptive.AlertItem;
using FYP.Xamarin.Mobile.Renders;
using System.Text.RegularExpressions;
using FYP.Xamarin.Mobile.Alerts;

namespace FYP.Xamarin.Mobile.ViewsModel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Alerts : ContentPage
	{
        private ActivityCacheHandler activityCacheHandler;
        public List<AlertItem> AlertItems;
        private int Seconds;
        private ObservableCollection<string> Items = new ObservableCollection<string> { "1 Second", "10 Seconds", "20 Seconds", "30 Seconds" };
        private Dictionary<int, string> AvailableDates;

        public Alerts(string athleteId, string accessToken)
        {
            InitializeComponent();
            IntialSetup(athleteId, accessToken);
            RefreshList();
            LoadScreen();
            Commands();
        }

        public void IntialSetup(string athleteId, string accessToken)
        {
            DataManipulatorHandler.CreateSingleton(athleteId, accessToken);
            activityCacheHandler = new ActivityCacheHandler();
            activityCacheHandler.Init(Int64.Parse(athleteId));
            Title = "Alerts";
            this.Seconds = 10;
        }

        public void Commands()
        {
            AlertListView.RefreshCommand = new Command(() => {
                LoadScreen();
                Task.Delay(1000);
                AlertListView.IsRefreshing = false;
            });
        }

        public void RefreshList()
        {
            AlertItems = new List<AlertItem>();
            DropDownBox.ItemsSource = Items;
        }

        public void LoadScreen()
        {
            AlertItems.Clear();
            AlertWatcher(Seconds);  
        }

        public async void AlertWatcher(int seconds)
        {
            AlertListView.IsVisible = false;
            Loading_Icon.IsVisible = true;
            this.AvailableDates = await DataManipulatorHandler.Instance.GetDates("Power");
            await CheckForNewAlertsAsync("Power", AvailableDates.Values.ElementAt(0), seconds);
            this.AvailableDates = await DataManipulatorHandler.Instance.GetDates("Speed");
            await CheckForNewAlertsAsync("Speed", AvailableDates.Values.ElementAt(0), seconds);
            this.AvailableDates = await DataManipulatorHandler.Instance.GetDates("Cadence");
            await CheckForNewAlertsAsync("Cadence", AvailableDates.Values.ElementAt(0), seconds);
            Loading_Icon.IsVisible = false;
            AlertListView.IsVisible = true;
            AlertListView.ItemsSource = AlertItems;
        }

        private void DropDownBox_SelectedItemChanged(object sender, Plugin.InputKit.Shared.Utils.SelectedItemChangedArgs e)
        {
            RefreshList();
            IfSelected("1 Second");
            IfSelected("10 Seconds");
            IfSelected("20 Seconds");
            IfSelected("30 Seconds");
        }

        public void IfSelected(string selectedSeconds)
        {
            if (DropDownBox.SelectedItem.ToString().Equals(selectedSeconds))
            {
                this.Seconds = Convert.ToInt32(Regex.Match(selectedSeconds, @"\d+").Value);
                LoadScreen();
            }
        }

        public async Task CheckForNewAlertsAsync(string node, string month, int seconds)//reverse this!
        {
            Dictionary<string, int> dailyTotals = await DataManipulatorHandler.Instance.GetDailyTopPeakAverages(node, month, seconds);
            var current = dailyTotals.Take(2).ElementAt(0);
            var previous = dailyTotals.Take(2).ElementAt(1);

            if (current.Value < previous.Value)
            {
                AlertItems.Add(new AlertItem(node + " has dropped! \n" + "Decreased by " +DataManipulatorHandler.Instance.GetPercentageDifference(current.Value, previous.Value) + "%")
                {
                    ChartData = new RadialGaugeChart()
                    {
                        Entries = new[]
                    {
                            new Microcharts.Entry(current.Value)
                            {
                                Label = current.Key,
                                ValueLabel = current.Value.ToString() + " " +LabelHandler.Instance.GetPeaksLabel(node),
                                Color = SKColor.Parse("#F21717")
                            },
                            new Microcharts.Entry(previous.Value)
                            {
                                Label = previous.Key,
                                ValueLabel = previous.Value.ToString() +" "+ LabelHandler.Instance.GetPeaksLabel(node),
                                Color = SKColor.Parse("#5894D0")
                            }
                    },
                        BackgroundColor = SKColors.White,
                        LabelTextSize = 25
                    }
                });
            }
        }

       

        private void AlertListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            if (((AlertItem)e.Item).alertMessage.Contains("Power has dropped!"))
            {
                AlertFactory.GetSingleton().CreateAlert(((AlertItem)e.Item).alertMessage);
            }

            if (((AlertItem)e.Item).alertMessage.Contains("Cadence has dropped!"))
            {
                AlertFactory.GetSingleton().CreateAlert(((AlertItem)e.Item).alertMessage);
            }
            if (((AlertItem)e.Item).alertMessage.Contains("Speed has dropped!"))
            {
                AlertFactory.GetSingleton().CreateAlert(((AlertItem)e.Item).alertMessage);
            }
        }


    }
}