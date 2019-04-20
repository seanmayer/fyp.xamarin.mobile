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

        public Activity(long activityId, long athleteId, string stravaid, string name, string startDate, string timeZone, string label1, string label2) : this(activityId, athleteId, stravaid, name, startDate, timeZone)
        {
            this.activityId = activityId;
            this.athleteId = athleteId;
            this.stravaid = stravaid;
            this.name = name;
            this.startDate = startDate;
            this.timeZone = timeZone;
            this.label1 = label1;
            this.label2 = label2;
        }

        [PrimaryKey]
        public long activityId { get; set; }

        [Indexed]
        public long athleteId { get; set; }

        public string stravaid { get; set; }

        public string name { get; set; }

        public string startDate { get; set; }

        public string timeZone { get; set; }

        public string label1 { get; set; }
        public string label2 { get; set; }

        public override string ToString()
        {
            return activityId + " " + athleteId + " " + stravaid + " " + name + " " + startDate + " " + timeZone;
        }
    }
}
