using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Services.Model
{
    public class ActivityRootObject : AbstractRootObject
    {
        public long activityId { get; set; }

        public AthleteId athleteId { get; set; }

        public string stravaid { get; set; }

        public string name { get; set; }

        public string startDate { get; set; }

        public string timeZone { get; set; }

        public class AthleteId
        {
            public long athleteId { get; set; }
        }

        public override bool isNil()
        {
            return false;
        }

    }
}
