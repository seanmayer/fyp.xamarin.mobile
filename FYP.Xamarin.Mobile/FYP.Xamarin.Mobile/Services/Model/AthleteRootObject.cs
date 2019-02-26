using FYP.Xamarin.Mobile.Database.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Services.Model
{
    public class AthleteRootObject
    {
        public long AthleteId { get; set; }

        public Credentials Credentials { get; set; }

        public string StravaId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
