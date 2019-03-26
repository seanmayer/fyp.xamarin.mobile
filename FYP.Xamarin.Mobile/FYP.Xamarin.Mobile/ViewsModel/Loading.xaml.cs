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
	public partial class Loading : ContentPage
	{
		public Loading ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }
	}
}