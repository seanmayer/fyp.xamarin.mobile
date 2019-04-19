using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FYP.Xamarin.Mobile.Renders
{
    public class ChartColourHandler
    {
        private static ChartColourHandler singletonInstance = CreateSingleton();
        private readonly string PowerColorScheme = "#EC5D5D";
        private readonly string SpeedColorScheme = "#7EBDD1";
        private readonly string CadenceColorScheme = "#A5C2A3";
        private readonly string DefaultColorScheme = "#707070";

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

        public SKColor GetSKColorCustomStyles(String menuSelection)
        {
            switch (menuSelection)
            {
                case "Power":
                    return SKColor.Parse(PowerColorScheme);
                case "Cadence":
                    return SKColor.Parse(CadenceColorScheme);
                case "Speed":
                    return SKColor.Parse(SpeedColorScheme);
                default:
                    return SKColor.Parse(DefaultColorScheme);
            }
        }

        public Color GetColorCustomStyles(String menuSelection)
        {
            switch (menuSelection)
            {
                case "Power":
                    return Color.FromHex(PowerColorScheme);
                case "Cadence":
                    return Color.FromHex(CadenceColorScheme);
                case "Speed":
                    return Color.FromHex(SpeedColorScheme);
                default:
                    return Color.FromHex(DefaultColorScheme);
            }
        }
    }
}
