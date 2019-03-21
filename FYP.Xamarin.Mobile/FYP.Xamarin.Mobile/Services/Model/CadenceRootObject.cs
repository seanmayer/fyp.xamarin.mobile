using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Services.Model
{
    public class CadenceRootObject
    {
        public CadenceRootObject(string cadencestream)
        {
            this.cadencestream = cadencestream;
        }
        public string cadencestream { get; set; }
    }
}
