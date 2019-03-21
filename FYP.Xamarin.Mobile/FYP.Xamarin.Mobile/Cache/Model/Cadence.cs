using SQLite;

namespace FYP.Xamarin.Mobile.Database.Model
{

    public class Cadence
    {

        public Cadence()
        {
        }

        public Cadence(long activityId, string stream)
        {
            this.activityId = activityId;
            this.stream = stream;
        }

        [PrimaryKey]
        [AutoIncrement]
        public long cadenceId { get; set; }

        public long activityId { get; set; }

        public string stream { get; set; }

        public override string ToString()
        {
            return cadenceId + " ";
        }
    }
}
