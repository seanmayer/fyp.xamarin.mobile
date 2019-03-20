
namespace FYP.Xamarin.Mobile.Services.Model.Null_Object
{
    public class NullActivityRootObject : AbstractRootObject
    {
        public NullActivityRootObject()
        {
            App.Current.MainPage.DisplayAlert("Alert", "Network failure", "OK");
        }

        public override bool isNil()
        {
            return true;
        }
    }
}
