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

namespace FYP.Xamarin.Mobile.ViewsModel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityAnaylsis : ContentPage
    {
        private PowerStreamServiceHandler powerStreamServiceHandler;
        private PowerCacheHandler powerCacheHandler;
        private long ActivityId;
        private List<Entry> Entries = new List<Entry>();
        private Dictionary <int, long> powerStream;
        //{
        //    new Entry(200)
        //    {
        //        Color=SKColor.Parse("#FF1943"),
        //        //Label ="January",
        //        //ValueLabel = "200"
        //    },
        //    new Entry(400)
        //    {
        //        Color = SKColor.Parse("00BFFF"),
        //        //Label = "March",
        //        //ValueLabel = "400"
        //    },
        //    };

        public ActivityAnaylsis(Activity activity, string accessToken)
        {
            InitializeComponent();
            Title = "Loading" + activity.activityId;
            this.ActivityId = activity.activityId;
            ApplyStyles();
            powerStreamServiceHandler = new PowerStreamServiceHandler();
            powerCacheHandler = new PowerCacheHandler();
            powerStreamServiceHandler.Init(activity.activityId.ToString(), activity.stravaid.ToString(), accessToken);
            SyncCachedPowerStream();
        }

        public void ApplyStyles()
        {
            BackgroundColor = Color.FromHex("#1F2D44");
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#1F2D44");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }

        public async Task<List<PowerRootObject>> RequestFindPowerStream()
        {
            try
            {
                List<Services.Model.PowerRootObject> x = await powerStreamServiceHandler.FindAll();
                return x;
            }
            catch (Exception e)
            {
                await DisplayAlert("Offline", "Unable to reach server" + e, "OK");
                return null;
            }
        }

        public async Task RequestCreatePowerStream()
        {
            try
            {
                await powerStreamServiceHandler.Create();
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.ToString(), "OK");
            }

        }

        public async Task<bool> CheckPowerCache()
        {
            List<Power> list = await powerCacheHandler.Find(ActivityId);
            if (list.Count() != 0){ await DisplayAlert("Done", "Exists", "OK"); return true;}
            else{ await DisplayAlert("Done", "Does not Exists", "OK"); return false;}     
        }

        public async Task<bool> CacheCreatePowerStream()
        {
            try
            {
                foreach (var power in await RequestFindPowerStream())
                {
                    powerCacheHandler.Init(ActivityId, power.powerstream);
                    await powerCacheHandler.Create();
                }
                await DisplayAlert("Done", "Created Power Cache", "OK");
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async void SyncCachedPowerStream()
        {
            await RequestCreatePowerStream();
            if (await CheckPowerCache() == false)
            {
                await CacheCreatePowerStream();
            }
            SetPowerCache();
           
        }

        public async void SetPowerCache()
        {
            foreach (Power p in await powerCacheHandler.Find(ActivityId))
            {
                this.powerStream = JsonConvert.DeserializeObject<Dictionary<int, long>>(p.stream);
            }
            RefreshChart();
        }

        public void RefreshChart()
        {
            foreach (KeyValuePair<int, long> entry in powerStream.OrderBy(d => d.Key).ToList())
            {
                Entries.Add(new Entry(entry.Value)
                {
                    Color = SKColor.Parse("#00CED1"),
                    //Label = entry.Value.ToString(),
                    //ValueLabel = randomNumber.ToString()

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