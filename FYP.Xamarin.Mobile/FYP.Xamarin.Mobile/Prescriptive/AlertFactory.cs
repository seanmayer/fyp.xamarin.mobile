using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Alerts
{
    public class AlertFactory
    {
        private static AlertFactory Instance;

        public AlertFactory()
        {

        }

        public static AlertFactory GetSingleton()
        {
            Instance = new AlertFactory();
            return Instance;
        }

        public void CreateAlert(string type)
        {
            switch (type)
            {
                case "Power":
                    break;
                case "Cadence":
                    break;
                case "Speed":
                    break;
                default:
                    break;
            }
        }
    }
}
