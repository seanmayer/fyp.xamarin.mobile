using FYP.Xamarin.Mobile.Database;
using FYP.Xamarin.Mobile.Services;
using FYP.Xamarin.Mobile.Services.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP.Xamarin.Mobile.ViewsModel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivitieList : ContentPage
    {
        private ActivityServiceHandler activityServiceHandler;
        private ActivityCacheHandler activityCacheHandler;

        public ObservableCollection<string> Items { get; set; }

        public ActivitieList(string athleteId, string stravaId, string accessToken)
        {
            InitializeComponent();
            activityServiceHandler = new ActivityServiceHandler();
            activityCacheHandler = new ActivityCacheHandler();
            Items = new ObservableCollection<string>{};
            LoadAllCachedActivities();
            activityServiceHandler.Init(athleteId, stravaId, accessToken);
        }

        public async Task CreateActivites()
        {
            try
            {
               await activityServiceHandler.Create();
               await DisplayAlert("Message", "Created Activities!", "OK");
            }
            catch (Exception e)
            {
               await DisplayAlert("Error", e.ToString(), "OK");
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
                await DisplayAlert("Error", e.ToString(), "OK");
                return null;
            }
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
            SyncCachedActivities();
            LoadAllCachedActivities();
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
            //need to do this based from the athlete ID!
            Items.Clear();
            foreach (var activity in await activityCacheHandler.FindAll())
            {
                Items.Add(activity.activityId +" : "+activity.name);

                MyListView.ItemsSource = Items;
            }
        }


    }
}
