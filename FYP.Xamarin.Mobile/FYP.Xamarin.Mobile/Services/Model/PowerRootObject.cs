using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Services.Model
{
    public class PowerRootObject
    {
        public PowerRootObject(string powerstream)
        {
            this.powerstream = powerstream;
        }
        public string powerstream { get; set; }
    }
}
