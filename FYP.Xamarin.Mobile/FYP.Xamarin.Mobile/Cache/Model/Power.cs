using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Database.Model
{

    public class Power
    {

        public Power()
        {
        }

        public Power(long activityId, string stream)
        {
            this.activityId = activityId;
            this.stream = stream;
        }

        [PrimaryKey]
        [AutoIncrement]
        public long powerId { get; set; }

        public long activityId { get; set; }

        public string stream { get; set; }

        public override string ToString()
        {
            return powerId + " ";
        }
    }
}
