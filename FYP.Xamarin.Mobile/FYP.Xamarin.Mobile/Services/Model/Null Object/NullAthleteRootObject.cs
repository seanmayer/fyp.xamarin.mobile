
namespace FYP.Xamarin.Mobile.Services.Model.Null_Object
{
    public class NullAthleteRootObject : AbstractRootObject
    {
        public NullAthleteRootObject()
        {
            App.Current.MainPage.DisplayAlert("Alert", "Network failure", "OK");
        }

        public override bool isNil()
        {
            return true;
        }
    }
}
