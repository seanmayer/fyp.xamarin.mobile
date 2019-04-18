using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FYP.Xamarin.Mobile.Formatters
{

    public class FormatterHandler
    {
        private static FormatterHandler singletonInstance = CreateSingleton();

        private FormatterHandler()
        {
        }

        private static FormatterHandler CreateSingleton()
        {
            if (singletonInstance == null)
            {
                singletonInstance = new FormatterHandler();
            }

            return singletonInstance;
        }

        public static FormatterHandler Instance
        {
            get { return singletonInstance; }
        }

        public DateTime ConvertGMTToDateTime(string date)
        {
            return DateTime.ParseExact(date, "ddd MMM dd HH:mm:ss 'GMT' yyyy", CultureInfo.InvariantCulture);
        }

        public string ConvertGMTToDDMMYYYY(string date)
        {
            return DateTime.ParseExact(date, "ddd MMM dd HH:mm:ss 'GMT' yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
        }

        public string ConvertGMTToMonth(string date)
        {
            return DateTime.ParseExact(date, "ddd MMM dd HH:mm:ss 'GMT' yyyy", CultureInfo.InvariantCulture).ToString("MMMM");
        }

        public string ConvertEpochTimeTohhmmssfff(double time)
        {
            return TimeSpan.FromSeconds(time).ToString(@"hh\:mm\:ss\:fff");
        }



    }
    
}
