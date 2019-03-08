using FYP.Xamarin.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP.Xamarin.Mobile.ViewsModel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivitieList : ContentPage
    {
        private ActivityServiceHandler activityServiceHandler;

        public ObservableCollection<string> Items { get; set; }

        public ActivitieList(string athleteId, string stravaId, string accessToken)
        {
            InitializeComponent();
            activityServiceHandler = new ActivityServiceHandler();
            Items = new ObservableCollection<string>
            {
                "Item 1",
                "Item 2",
                "Item 3",
                "Item 4",
                "Item 5"
            };

            MyListView.ItemsSource = Items;
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

        public async Task FindAllActivities()
        {
            try
            {
                List<Services.Model.ActivityRootObject> x = await activityServiceHandler.FindAll();

                foreach (var money in x)
                {
                    await DisplayAlert("Message", money.name, "OK");
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.ToString(), "OK");
            }
            await DisplayAlert("Message", "Done!", "OK");
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
            await CreateActivites();
            await FindAllActivities();

            //Cache system next
            // then can display activities

        }
    }
}
