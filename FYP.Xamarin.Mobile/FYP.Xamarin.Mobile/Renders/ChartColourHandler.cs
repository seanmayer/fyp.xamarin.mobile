using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Renders
{
    public class ChartColourHandler
    {
        private static ChartColourHandler singletonInstance = CreateSingleton();

        private ChartColourHandler()
        {
        }

        private static ChartColourHandler CreateSingleton()
        {
            if (singletonInstance == null)
            {
                singletonInstance = new ChartColourHandler();
            }

            return singletonInstance;
        }

        public static ChartColourHandler Instance
        {
            get { return singletonInstance; }
        }

        public string GetCustomStyles(String menuSelection)
        {
            switch (menuSelection)
            {
                case "Power":
                    return "#EC5D5D";
                case "Cadence":
                    return "#A5C2A3";
                case "Speed":
                    return "#7EBDD1";
                default:
                    return "#707070";
            }
        }
    }
}
