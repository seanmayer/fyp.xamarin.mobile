using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Services.Model.Null_Object
{
    public class NullCadenceRootObject
    {
        public NullCadenceRootObject()
        {
            App.Current.MainPage.DisplayAlert("Alert", "Network failure", "OK");
        }
        public string cadencestream { get; set; }
    }
}
