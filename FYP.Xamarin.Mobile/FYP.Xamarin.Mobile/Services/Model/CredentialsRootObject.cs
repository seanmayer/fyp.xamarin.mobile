using FYP.Xamarin.Mobile.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Json
{
    public class CredentialsRootObject : AbstractRootObject
    {
        public long credentialsId { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public override bool isNil()
        {
            return false;
        }
    }
}
