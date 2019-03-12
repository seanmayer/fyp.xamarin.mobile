using FYP.Xamarin.Mobile.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP.Xamarin.Mobile.ViewsModel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActivityMenu : ContentPage
	{
		public ActivityMenu ()
		{
			InitializeComponent ();
            Title = "Loading";
            ApplyStyles();
        }

        public ActivityMenu(Activity activity)
        {
            InitializeComponent();
            Title = activity.name;
            ApplyStyles();
        }

        public void ApplyStyles()
        {
            BackgroundColor = Color.FromHex("#1F2D44");
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#1F2D44");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;

            var redButton = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = Button.BackgroundColorProperty, Value = Color.FromHex ("#EC5D5D") },
                    new Setter { Property = Button.TextColorProperty, Value = Color.White },
                    new Setter { Property = Button.BorderRadiusProperty, Value = 5 },
                    new Setter { Property = Button.FontSizeProperty, Value = 30 }
                }
            };

            var greenButton = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = Button.BackgroundColorProperty, Value = Color.FromHex ("#A5C2A3") },
                    new Setter { Property = Button.TextColorProperty, Value = Color.White },
                    new Setter { Property = Button.BorderRadiusProperty, Value = 5 },
                    new Setter { Property = Button.FontSizeProperty, Value = 30 }
                }
            };

            var blueButton = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = Button.BackgroundColorProperty, Value = Color.FromHex ("#7EBDD1") },
                    new Setter { Property = Button.TextColorProperty, Value = Color.White },
                    new Setter { Property = Button.BorderRadiusProperty, Value = 5 },
                    new Setter { Property = Button.FontSizeProperty, Value = 30 }
                }
            };

            var controlGrid = new Grid { RowSpacing = 15, ColumnSpacing = 15 };
            controlGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0) });
            controlGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            controlGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            controlGrid.Children.Add(new Button { Text = "Power", Style = redButton }, 0, 1);
            controlGrid.Children.Add(new Button { Text = "Cadence", Style = greenButton }, 1, 1);
            controlGrid.Children.Add(new Button { Text = "Speed", Style = blueButton }, 0, 2);
            Content = controlGrid;
        }
	}
}