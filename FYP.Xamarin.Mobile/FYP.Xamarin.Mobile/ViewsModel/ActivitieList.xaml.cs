using FYP.Xamarin.Mobile.Database;
using FYP.Xamarin.Mobile.Database.Model;
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
        public ObservableCollection<Activity> Items { get; set; }
        public string AccessToken;


        public ActivitieList(string athleteId, string stravaId, string accessToken)
        {
            InitializeComponent();
            ApplyStyles();
            NavigationPage.SetHasNavigationBar(this, true);
            this.AccessToken = accessToken;
            activitySummaryServiceHandler = new ActivitySummaryServiceHandler();
            activityServiceHandler = new ActivityServiceHandler();
            activityCacheHandler = new ActivityCacheHandler();
            Items = new ObservableCollection<Activity> {};
            activitySummaryServiceHandler.Init(stravaId, accessToken);
            activityServiceHandler.Init(athleteId, stravaId, accessToken);
            activityCacheHandler.Init(Int64.Parse(athleteId));
            SyncCachedActivities();
            LoadAllCachedActivities();
        }

        public void ApplyStyles()
        {
            NavigationPage.SetHasBackButton(this, false);
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#1F2D44");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }

        public async Task CreateActivites()
        {
            try
            {
               await activityServiceHandler.Create();
               await activitySummaryServiceHandler.Create();
               //await DisplayAlert("Message", "Created Activities!", "OK");
            }
            catch (Exception e)
            {
               //await DisplayAlert("Error", e.ToString(), "OK");
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

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            await Navigation.PushAsync(new ActivityMenu((Activity)e.Item, AccessToken));
            ((ListView)sender).SelectedItem = null;//Deselect Item
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

            int count = 1;
            foreach (var activity in await activityCacheHandler.FindAll())
            {
                try
                { 
                    DateTime dt = DateTime.ParseExact(activity.startDate, "ddd MMM dd HH:mm:ss 'GMT' yyyy", CultureInfo.InvariantCulture);
                    activity.label = dt.ToString("dd/MM/yyyy");
                    Items.Add(activity);
                    count++;
                }
                catch(Exception e)
                {
                    activity.label = activity.name;
                    Items.Add(activity);
                }
            }

            MyListView.ItemsSource = Items;
        }


    }
}
