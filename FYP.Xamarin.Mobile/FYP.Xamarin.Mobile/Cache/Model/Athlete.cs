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

        public Athlete(long athleteId, long credentialsId, string stravaId, string accessToken, string firstName, string lastName)
        {
            AthleteId = athleteId;
            CredentialsId = credentialsId;
            StravaId = stravaId;
            AccessToken = accessToken;
            FirstName = firstName;
            LastName = lastName;
        }

        [PrimaryKey]
        public long AthleteId { get; set; }

        [Indexed]
        public long CredentialsId { get; set; }

        public string StravaId { get; set; }

        public string AccessToken { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


        public override string ToString()
        {
            return CredentialsId + " " + StravaId + " " + FirstName + " " + LastName;
        }

    }
}
