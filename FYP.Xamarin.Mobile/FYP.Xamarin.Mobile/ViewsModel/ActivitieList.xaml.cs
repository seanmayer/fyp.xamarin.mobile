using FYP.Xamarin.Mobile.Database;
using FYP.Xamarin.Mobile.Database.Model;
using FYP.Xamarin.Mobile.Formatters;
using FYP.Xamarin.Mobile.Services;
using FYP.Xamarin.Mobile.Services.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP.Xamarin.Mobile.ViewsModel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivitieList : ContentPage
    {
        private ActivitySummaryServiceHandler activitySummaryServiceHandler;
        private ActivityServiceHandler activityServiceHandler;
        private ActivityCacheHandler activityCacheHandler;
        private ActivitySummaryCacheHandler activitySummaryCacheHandler;
        public ObservableCollection<Activity> Items { get; set; }
        public string AccessToken;


        public ActivitieList(string athleteId, string stravaId, string accessToken)
        {
            InitializeComponent();
            ApplyStyles();
            NavigationPage.SetHasNavigationBar(this, true);
            this.AccessToken = accessToken;
            Items = new ObservableCollection<Activity> {};
            InitliseServiceAndCache(athleteId, stravaId, accessToken);
            SyncCachedActivities();
            SyncCachedActivitySummaries();
            LoadAllCachedActivities();

            
        }

        public void InitliseServiceAndCache(string athleteId, string stravaId, string accessToken)
        {
            activitySummaryServiceHandler = new ActivitySummaryServiceHandler();
            activitySummaryCacheHandler = new ActivitySummaryCacheHandler();
            activityServiceHandler = new ActivityServiceHandler();
            activityCacheHandler = new ActivityCacheHandler();
            activitySummaryServiceHandler.Init(athleteId, stravaId, accessToken);
            activityServiceHandler.Init(athleteId, stravaId, accessToken);
            activityCacheHandler.Init(Int64.Parse(athleteId));
        }

        public void ApplyStyles()
        {
            NavigationPage.SetHasBackButton(this, false);
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#1F2D44");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }

        void OnSwiped(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {
                case SwipeDirection.Down:

                    MyListView.BackgroundColor = Color.Default;
                    LoadAllCachedActivities();
                    break;
            }
        }

        public async Task CreateActivites()
        {
            try
            {
               await activityServiceHandler.Create();
            }
            catch (Exception e)
            {
                await DisplayAlert("Offline", "CreateActivites" + e, "OK");
            }
            
        }


        public async Task CreateActivitySummaries()
        {
            try
            {
                await activitySummaryServiceHandler.Create();
            }
            catch (Exception e)
            {
                await DisplayAlert("Offline", "CreateActivitySummaries" + e, "OK");
            }

        }

        public async Task<List<ActivityRootObject>> FindAllActivities()
        {
            try
            {
                List<Services.Model.ActivityRootObject> x = await activityServiceHandler.FindAll();
                return x;
            }
            catch (Exception e)
            {
                await DisplayAlert("Offline", "Unable to reach server", "OK");
                return null;
            }
        }

        public async Task<List<ActivitySummaryRootObject>> FindAllActivitySummaries()
        {
            try
            {
                List<Services.Model.ActivitySummaryRootObject> x = await activitySummaryServiceHandler.FindAll();
                return x;
            }
            catch (Exception e)
            {
                await DisplayAlert("Offline", "Unable to reach server" + e, "OK");
                return null;
            }
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            await Navigation.PushAsync(new ActivityMenu((Activity)e.Item, AccessToken));
            ((ListView)sender).SelectedItem = null;//Deselect Item
        }

        public async void SyncCachedActivitySummaries()
        {
            try
            {
                await CreateActivitySummaries();

                foreach (var activitySummary in await FindAllActivitySummaries())
                {
                    activitySummaryCacheHandler.Init(activitySummary.activitySummaryId, activitySummary.movingTime, activitySummary.distance,activitySummary.maxSpeed, activitySummary.maxWatts,activitySummary.averageSpeed, activitySummary.averageWatts,activitySummary.averageCadence, activitySummary.kilojoules, activitySummary.activityId.activityId);
                    await activitySummaryCacheHandler.Create();
                }

            }
            catch (Exception e)
            {
                await DisplayAlert("Error", "Sync Failure" + e, "OK");
            }
        }


        public async void SyncCachedActivities()
        {
            try
            {
                await CreateActivites();
                foreach (var activity in await FindAllActivities())
                {
                    activityCacheHandler.Init(activity.activityId, activity.athleteId.athleteId, activity.stravaid, activity.name, activity.startDate, activity.timeZone);
                    await activityCacheHandler.Create();
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Sync Failure", "OK");
            }


        }

        public async void LoadAllCachedActivities()
        {
            Items.Clear();
            await Task.Delay(5000);
            MyListView.BackgroundColor = Color.FromHex("#ffffff");
            foreach (var activity in await activityCacheHandler.FindAll())
            {
                try
                { 
                    ActivitySummary activitySummary = await activitySummaryCacheHandler.Find(activity.activityId);
                    activity.label1 = FormatterHandler.Instance.ConvertGMTToDDMMYYYY(activity.startDate);
                    activity.label2 = "Moving Time: " +FormatterHandler.Instance.ConvertEpochTimeTohhmmssfff(Convert.ToDouble(activitySummary.movingTime));
                    Items.Add(activity);
                }
                catch(Exception e)
                {
                    activity.label1 = activity.name;
                    Items.Add(activity);
                }
            }

            MyListView.ItemsSource = Items;
        }


    }
}
