using System;
using System.Collections.Generic;
using System.Text;

namespace FYP.Xamarin.Mobile.Errors
{
    public class ErrorHandler
    {
        private static ErrorHandler singletonInstance = CreateSingleton();

        private ErrorHandler()
        {
        }

        private static ErrorHandler CreateSingleton()
        {
            if (singletonInstance == null)
            {
                singletonInstance = new ErrorHandler();
            }

            return singletonInstance;
        }

        public static ErrorHandler Instance
        {
            get { return singletonInstance; }
        }

        public string CheckStreamSequenceNotOutAbounds(int streamLabelValue)
        {
            if (streamLabelValue == -1)
            {
                return "N/A";
            }

            return streamLabelValue.ToString();
        }
    }
}
