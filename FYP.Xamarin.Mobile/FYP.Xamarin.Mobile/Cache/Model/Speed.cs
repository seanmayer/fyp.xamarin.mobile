using SQLite;


namespace FYP.Xamarin.Mobile.Database.Model
{

    public class Speed
    {

        public Speed()
        {
        }

        public Speed(long activityId, string stream)
        {
            this.activityId = activityId;
            this.stream = stream;
        }

        [PrimaryKey]
        [AutoIncrement]
        public long speedId { get; set; }

        public long activityId { get; set; }

        public string stream { get; set; }

        public override string ToString()
        {
            return speedId + " ";
        }
    }
}
