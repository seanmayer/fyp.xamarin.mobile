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

namespace FYP.Xamarin.Mobile.ViewsModel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActivityAnaylsis : ContentPage
	{
        private PowerStreamServiceHandler powerStreamServiceHandler;


        List<Entry> entries = new List<Entry>
        {
            new Entry(200)
            {
                Color=SKColor.Parse("#FF1943"),
                //Label ="January",
                //ValueLabel = "200"
            },
            new Entry(400)
            {
                Color = SKColor.Parse("00BFFF"),
                //Label = "March",
                //ValueLabel = "400"
            },
            };




        public ActivityAnaylsis()
		{
			InitializeComponent();
            Title = "Loading";
            BackgroundColor = Color.FromHex("#1F2D44");
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#1F2D44");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            powerStreamServiceHandler = new PowerStreamServiceHandler();
            powerStreamServiceHandler.Init("1984914419", "35193560", "e8a14408cd001cb6a86607a21ff50bd42f0b76f8");
            SyncCachedPowerStream();

            for (int i = 0; i < 100; i++)
            {
                Random random = new Random();
                int randomNumber = random.Next(i*2, i*10);

                entries.Add(new Entry(randomNumber)
                {
                    Color = SKColor.Parse("#00CED1"),
                    //Label = "Octobar",
                    //ValueLabel = randomNumber.ToString()

                });
            }

            Chart2.Chart = new LineChart()
            {
                Entries = entries,
                LineMode = LineMode.Straight,
                LineSize = 1,
                PointMode = PointMode.None,
                PointSize = 1,
            };
        }

        public async Task<List<PowerRootObject>> FindAllPowerStream()
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

        public async Task CreatePowerStream()
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

        public async void SyncCachedPowerStream()
        {
            await CreatePowerStream();
            //foreach (var power in await FindAllPowerStream())
            //{
            //    powerStreamCacheHandler.Init(activity.activityId, activity.athleteId.athleteId, activity.stravaid, activity.name, activity.startDate, activity.timeZone);
            //    await powerStreamCacheHandler.Create();
            //}

            //List<PowerRootObject> powerstream = await FindAllPowerStream();
            //await DisplayAlert("Test", powerstream.ToString(), "OK");
        }
    }
}