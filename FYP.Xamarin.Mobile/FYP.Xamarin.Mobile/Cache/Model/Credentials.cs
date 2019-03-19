using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Database.Tables
{
    public class Credentials
    {
        public Credentials()
        {
        }

        public Credentials(long credentialsId, string username, string password)
        {
            CredentialsId = credentialsId;
            Username = username;
            Password = password;
        }

        [PrimaryKey]
        public long CredentialsId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public override string ToString()
        {
            return CredentialsId + " " + Username + " " + Password;
        }

    }
}
