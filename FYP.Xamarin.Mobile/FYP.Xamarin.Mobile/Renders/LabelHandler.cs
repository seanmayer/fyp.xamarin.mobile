using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Renders
{
    public class LabelHandler
    {
        private static LabelHandler singletonInstance = CreateSingleton();

        private LabelHandler()
        {
        }

        private static LabelHandler CreateSingleton()
        {
            if (singletonInstance == null)
            {
                singletonInstance = new LabelHandler();
            }

            return singletonInstance;
        }

        public static LabelHandler Instance
        {
            get { return singletonInstance; }
        }


        public string GetPeaksTitle(String menuSelection)
        {
            switch (menuSelection)
            {
                case "Power":
                    return "Power Peaks "; 

                case "Cadence":
                    return "Cadence Peaks "; 

                case "Speed":
                    return "Speed Peaks "; 

                default:
                    return "Unavailable"; 

            }
        }

        public string GetPeaksLabel(String menuSelection)
        {
            switch (menuSelection)
            {
                case "Power":
                    return "watts";

                case "Cadence":
                    return  "rpm";

                case "Speed":
                    return "mph"; 

                default:
                    return  "n/a"; 

            }
        }


                
            
    }
}
