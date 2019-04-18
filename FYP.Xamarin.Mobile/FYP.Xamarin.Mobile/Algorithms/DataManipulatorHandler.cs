using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYP.Xamarin.Mobile.Algorithms
{
    public class DataManipulatorHandler
    {
        private static volatile DataManipulatorHandler singletonInstance = CreateSingleton();

        private DataManipulatorHandler()
        {
        }

        private static DataManipulatorHandler CreateSingleton()
        {
            if (singletonInstance == null)
            {
                singletonInstance = new DataManipulatorHandler();
            }

            return singletonInstance;
        }

        public static DataManipulatorHandler Instance
        {
            get { return singletonInstance; }
        }

        public double GetHighestSequenceXAverage(int Seconds, Dictionary<int, long> Stream)
        {
            try
            {
                Dictionary<int, double> Averages = new Dictionary<int, double>();

                decimal numberOfGroups = Stream.Keys.Max() / Seconds;
                int counter = 0;
                int groupSize = Convert.ToInt32(Math.Ceiling(Stream.Count / numberOfGroups));

                IEnumerable<IGrouping<int, KeyValuePair<int, long>>> groupedData = Stream.GroupBy(x => counter++ / groupSize);

                foreach (var group in groupedData)
                {
                    Averages.Add(group.Key, group.Average(t => t.Value));
                }

                return Averages.Values.Max();

            }
            catch (Exception e)
            {
                return -1;
            }
        }
    }
}
