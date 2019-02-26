using FYP.Xamarin.Mobile.Database.Tables;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Database.Model
{
    public class Athlete
    {
        public Athlete()
        {
        }

        public Athlete(Credentials credentials, string stravaId, string firstName, string lastName)
        {
            Credentials = credentials;
            StravaId = stravaId;
            FirstName = firstName;
            LastName = lastName;
        }

        [PrimaryKey, AutoIncrement]
        public long AthleteId { get; set; }

        public Credentials Credentials { get; set; }

        public string StravaId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


        public override string ToString()
        {
            return Credentials.ToString() + " " + StravaId + " " + FirstName + " " + LastName;
        }

    }
}
