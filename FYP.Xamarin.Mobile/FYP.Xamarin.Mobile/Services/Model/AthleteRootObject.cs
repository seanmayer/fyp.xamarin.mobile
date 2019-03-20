using FYP.Xamarin.Mobile.Database.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Services.Model
{
    public class AthleteRootObject : AbstractRootObject
    {
        public long athleteId { get; set; }

        public string stravaId { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }

        public override bool isNil()
        {
            return false;
        }
    }
}
