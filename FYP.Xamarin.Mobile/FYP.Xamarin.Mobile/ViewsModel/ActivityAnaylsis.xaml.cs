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

namespace FYP.Xamarin.Mobile.ViewsModel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActivityAnaylsis : ContentPage
	{

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
    }
}