using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Services.Model
{
    public class ActivityRootObject
    {
        public long activityId { get; set; }

        public AthleteId athleteId { get; set; }

        public string stravaid { get; set; }

        public string name { get; set; }

        public string startDate { get; set; }

        public string timeZone { get; set; }

        public override string ToString()
        {
            return activityId + " " + athleteId;
        }

        public class AthleteId
        {
            public long athleteId { get; set; }
        }
    }
}
