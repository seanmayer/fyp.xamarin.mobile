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
using Plugin.InputKit.Shared.Controls;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace FYP.Xamarin.Mobile.ViewsModel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TrendAnalysis : ContentPage
	{
        
        private string MenuSelection;
        private string AccessToken;
        private int Seconds;
        private ObservableCollection<string> Items = new ObservableCollection<string> { "1 Second", "10 Seconds", "20 Seconds", "30 Seconds" };

        public TrendAnalysis (string athleteId, string stravaId, string accessToken, string menuSelection)
		{
			InitializeComponent ();
            DataManipulatorHandler.CreateSingleton(athleteId, accessToken);
            this.MenuSelection = menuSelection;
            this.AccessToken = accessToken;
            this.Seconds = 10;
            LoadScreen();
        }

        public async void LoadScreen()
        {
            Title = MenuSelection;
            DropDownBox.ItemsSource = Items;
            MonthTitle1.TextColor = ChartColourHandler.Instance.GetColorCustomStyles(MenuSelection);
            MonthTitle2.TextColor = ChartColourHandler.Instance.GetColorCustomStyles(MenuSelection);
            Highlight1.BackgroundColor = ChartColourHandler.Instance.GetColorCustomStyles(MenuSelection);
            Highlight2.BackgroundColor = ChartColourHandler.Instance.GetColorCustomStyles(MenuSelection);
            
            Chart3.Chart = new LineChart()
            {
                Entries = await LoadChart(DataManipulatorHandler.Instance.PeakAverage(MenuSelection,"December", Seconds)),
                LineMode = LineMode.Straight, LineSize = 8, PointMode = PointMode.Square, PointSize = 18, BackgroundColor = SKColors.White
            };
            Chart3.IsVisible = true;
            
            Chart4.Chart = new LineChart()
            {
                Entries = await LoadChart(DataManipulatorHandler.Instance.PeakAverage(MenuSelection, "November", Seconds)),
                LineMode = LineMode.Straight, LineSize = 8, PointMode = PointMode.Square, PointSize = 18, BackgroundColor = SKColors.White
            };
            Chart4.IsVisible = true;

        }


        public async Task<List<Entry>> LoadChart(Task<Dictionary<long, int>> stream)
        {
            List<Entry> Entries = new List<Entry>();
            foreach (KeyValuePair<long, int> entry in await stream)
            {
                Entries.Add(new Entry(entry.Value)
                {
                    Color = ChartColourHandler.Instance.GetSKColorCustomStyles(MenuSelection),
                    Label = "|",
                    TextColor = ChartColourHandler.Instance.GetSKColorCustomStyles(MenuSelection),
                    ValueLabel = entry.Value.ToString()
                });
            }
            return Entries;
        }

        private void DropDownBox_SelectedItemChanged(object sender, Plugin.InputKit.Shared.Utils.SelectedItemChangedArgs e)
        {
            ChartWatcher("1 Second");
            ChartWatcher("10 Seconds");
            ChartWatcher("20 Seconds");
            ChartWatcher("30 Seconds");
        }

        public void ChartWatcher(string selectedSeconds)
        {
            if (DropDownBox.SelectedItem.ToString().Equals(selectedSeconds))
            {
                HideCharts();
                this.Seconds = Convert.ToInt32(Regex.Match(selectedSeconds, @"\d+").Value); 
                LoadScreen();
            }

        }

        public void HideCharts()
        {
            Chart3.IsVisible = false;
            Chart4.IsVisible = false;
        }
    }
}