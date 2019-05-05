using FYP.Xamarin.Mobile.Prescriptive;
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
                case "Power has dropped!":
                    App.Current.MainPage.DisplayAlert("Possible actions", new LowPowerAlert().AlertMessage, "OK");
                    break;
                case "Cadence has dropped!":
                    App.Current.MainPage.DisplayAlert("Possible actions", new LowCadenceAlert().AlertMessage, "OK");
                    break;
                case "Speed has droppped!":
                    App.Current.MainPage.DisplayAlert("Possible actions", new LowSpeedCadence().AlertMessage, "OK");
                    break;
                default:
                    break;
            }
        }
    }
}
