using FYP.Xamarin.Mobile.Database.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP.Xamarin.Mobile.ViewsModel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Alerts : ContentPage
	{

        public ObservableCollection<Activity> Items { get; set; }

        public Alerts ()
        {
            InitializeComponent();
            Items = new ObservableCollection<Activity>{new Activity(1,1,"1","name","10/12/2018","Time Zone","Test","Test")};
            Title = "Alerts";

            AlertListView.ItemsSource = Items;
        }

        private void AlertListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}