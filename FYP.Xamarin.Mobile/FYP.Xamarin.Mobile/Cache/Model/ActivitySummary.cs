using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Database.Model
{
    public class ActivitySummary
    {
        public ActivitySummary()
        {
        }

        public ActivitySummary(long activitySummaryId, string movingTime, string distance, string maxSpeed, string maxWatts, string averageSpeed, string averageWatts, string averageCadence, string kilojoules, long activityId)
        {
            this.activitySummaryId = activitySummaryId;
            this.movingTime = movingTime;
            this.distance = distance;
            this.maxSpeed = maxSpeed;
            this.maxWatts = maxWatts;
            this.averageSpeed = averageSpeed;
            this.averageWatts = averageWatts;
            this.averageCadence = averageCadence;
            this.kilojoules = kilojoules;
            this.activityId = activityId;
        }

        [PrimaryKey]
        public long activitySummaryId { get; set; }

        [Indexed]
        public long activityId { get; set; }
        public string movingTime { get; set; }
        public string distance { get; set; }
        public string maxSpeed { get; set; }
        public string maxWatts { get; set; }
        public string averageSpeed { get; set; }
        public string averageWatts { get; set; }
        public string averageCadence { get; set; }
        public string kilojoules { get; set; }

        public override string ToString()
        {
            return activityId + " " + movingTime + " " + distance + " " + maxSpeed + " " + maxWatts + " " + averageSpeed + " " + averageWatts + " " + averageCadence + " " + kilojoules;
        }
    }
}
