using Microcharts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Prescriptive
{
    public class AlertItem
    {
        public AlertItem(string alertMessage)
        {
            this.alertMessage = alertMessage;
        }

        public string alertMessage { get; set; }
        public Chart ChartData { get; set; }

    }
}
