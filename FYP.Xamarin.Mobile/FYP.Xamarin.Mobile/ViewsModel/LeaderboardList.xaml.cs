using FYP.Xamarin.Mobile.Algorithms;
using FYP.Xamarin.Mobile.Database;
using FYP.Xamarin.Mobile.Database.Model;
using FYP.Xamarin.Mobile.Errors;
using FYP.Xamarin.Mobile.Formatters;
using FYP.Xamarin.Mobile.Streams.StreamFactory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using FYP.Xamarin.Mobile.Renders;
using System.Text.RegularExpressions;

namespace FYP.Xamarin.Mobile.ViewsModel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LeaderboardList : ContentPage
	{
        private ActivityCacheHandler activityCacheHandler;
        private ActivitySummaryCacheHandler activitySummaryCacheHandler;
        private Dictionary<long, int> LeaderboardData;
        private ObservableCollection<string> DropDownBoxItems = new ObservableCollection<string> { "1 Second", "10 Seconds", "20 Seconds", "30 Seconds" };
        public ObservableCollection<Activity> Items { get; set; }
        private string MenuSelection;
        private string AccessToken;
        private int Seconds;


        public LeaderboardList (string athleteId, string stravaId, string accessToken, string menuSelection)
		{
			InitializeComponent ();
            Title = "Leaderboard";
            Items = new ObservableCollection<Activity>{};
            DataManipulatorHandler.CreateSingleton(athleteId,accessToken);
            
            activitySummaryCacheHandler = new ActivitySummaryCacheHandler();
            activityCacheHandler = new ActivityCacheHandler();

            this.Seconds = 10;
            this.MenuSelection = menuSelection;
            this.AccessToken = accessToken;
            LoadScreen();


        }

        public async void LoadScreen()
        {
            DropDownBox.ItemsSource = DropDownBoxItems;
            this.LeaderboardData =  await DataManipulatorHandler.Instance.PeakAverage(MenuSelection, "all", Seconds);
            var leaderboardDataSorted = LeaderboardData.ToList();
          

            int rank = 1;
            foreach (KeyValuePair<long, int> leaderboardEntry in from entry in leaderboardDataSorted orderby entry.Value descending select entry)
            {
                ActivitySummary activitySummary = await activitySummaryCacheHandler.Find(leaderboardEntry.Key);
                Activity activity = await activityCacheHandler.Find(leaderboardEntry.Key);
                string startDate = FormatterHandler.Instance.ConvertGMTToDDMMYYYY(activity.startDate);
                string movingtime = FormatterHandler.Instance.ConvertEpochTimeTohhmmssfff(Convert.ToDouble(activitySummary.movingTime));

                Items.Add(new Activity(leaderboardEntry.Key, IconSelector(rank++), leaderboardEntry.Value.ToString() + " " + LabelHandler.Instance.GetPeaksLabel(MenuSelection), startDate + "\n" + "Moving Time: " +movingtime));
            }
                Loading_Icon.IsVisible = false;
                Loading_Icon.IsPlaying = false;
                Loading_Icon.IsEnabled = false;
       
                LeaderboardListView.ItemsSource = Items;
        }

        public string IconSelector(int rank)
        {
            if(rank == 1)
            {
                return "scp_gold.png";
            }
            if(rank ==2)
            {
                return "scp_silver.png";
            }
            if(rank ==3)
            {
                return "scp_bronze.png";
            }
            return "scp_generic_icon.png";
        }


        private async void LeaderboardListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            Activity activity = await activityCacheHandler.Find(((Activity)e.Item).activityId);
            await Navigation.PushAsync(new ActivityMenu(activity, AccessToken));
            ((ListView)sender).SelectedItem = null;//Deselect Item
        }

        private void DropDownBox_SelectedItemChanged(object sender, Plugin.InputKit.Shared.Utils.SelectedItemChangedArgs e)
        {
            LeaderboardWatcher("1 Second");
            LeaderboardWatcher("10 Seconds");
            LeaderboardWatcher("20 Seconds");
            LeaderboardWatcher("30 Seconds");
        }

        public void LeaderboardWatcher(string selectedSeconds)
        {
            if (DropDownBox.SelectedItem.ToString().Equals(selectedSeconds))
            {
                Items.Clear();
                this.Seconds = Convert.ToInt32(Regex.Match(selectedSeconds, @"\d+").Value);
                LoadScreen();
            }

        }
    }
}