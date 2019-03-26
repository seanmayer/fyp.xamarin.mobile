using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Database.Model
{
    public class Activity
    {
        public Activity()
        {
        }

        public Activity(long activityId, long athleteId, string stravaid, string name, string startDate, string timeZone)
        {
            this.activityId = activityId;
            this.athleteId = athleteId;
            this.stravaid = stravaid;
            this.name = name;
            this.startDate = startDate;
            this.timeZone = timeZone;
        }

        [PrimaryKey]
        public long activityId { get; set; }

        [Indexed]
        public long athleteId { get; set; }

        public string stravaid { get; set; }

        public string name { get; set; }

        public string startDate { get; set; }

        public string timeZone { get; set; }

        public string label { get; set; }

        public override string ToString()
        {
            return activityId + " " + athleteId + " " + stravaid + " " + name + " " + startDate + " " + timeZone;
        }
    }
}
