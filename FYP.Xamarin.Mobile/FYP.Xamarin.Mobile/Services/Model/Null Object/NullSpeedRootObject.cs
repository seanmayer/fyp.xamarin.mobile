using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Services.Model.Null_Object
{
    public class NullSpeedRootObject
    {
        public NullSpeedRootObject()
        {
            App.Current.MainPage.DisplayAlert("Alert", "Network failure", "OK");
        }
        public string speedstream { get; set; }
    }
}
