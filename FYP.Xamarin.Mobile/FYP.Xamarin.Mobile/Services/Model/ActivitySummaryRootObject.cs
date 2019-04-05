using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Services.Model
{
    public class ActivitySummaryRootObject : AbstractRootObject
    {
        public long activitySummaryId { get; set; }

        public ActivityId activityId { get; set; }

        public string kilojoules { get; set; }
        public string averageCadence { get; set; }
        public string distance { get; set; }
        public string movingTime { get; set; }
        public string averageSpeed { get; set; }
        public string maxSpeed { get; set; }
        public string maxWatts { get; set; }
        public string averageWatts { get; set; }

        public class ActivityId
        {
            public long activityId { get; set; }
        }

        public override bool isNil()
        {
            return false;
        }





    }
}
