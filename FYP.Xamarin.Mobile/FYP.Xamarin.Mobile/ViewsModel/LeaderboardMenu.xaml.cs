using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP.Xamarin.Mobile.ViewsModel
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LeaderboardMenu : ContentPage
	{
		public LeaderboardMenu ()
		{
			InitializeComponent ();
            Title = "Leaderboard";
        }
	}
}