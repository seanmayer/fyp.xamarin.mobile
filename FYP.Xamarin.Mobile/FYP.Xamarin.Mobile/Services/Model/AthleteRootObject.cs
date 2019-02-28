using FYP.Xamarin.Mobile.Database.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Services.Model
{
    public class AthleteRootObject
    {
        public long athleteId { get; set; }

        public string stravaId { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }

        public override string ToString()
        {
            return stravaId + " " + firstname + " " + lastname;
        }
    }
}
