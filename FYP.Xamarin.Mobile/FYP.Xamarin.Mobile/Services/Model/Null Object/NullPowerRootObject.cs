
namespace FYP.Xamarin.Mobile.Services.Model.Null_Object
{
    public class NullPowerRootObject : AbstractRootObject
    {
        public NullPowerRootObject()
        {
            App.Current.MainPage.DisplayAlert("Alert", "Network failure", "OK");
        }

        public override bool isNil()
        {
            return true;
        }
    }
}
