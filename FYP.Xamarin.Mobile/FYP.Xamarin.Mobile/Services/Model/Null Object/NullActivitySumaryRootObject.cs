using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Services.Model.Null_Object
{
    public class NullActivitySumaryRootObject : AbstractRootObject
    {
        public NullActivitySumaryRootObject()
        {
            App.Current.MainPage.DisplayAlert("Alert", "Network failure", "OK");
        }

        public override bool isNil()
        {
            return true;
        }
    }
}
