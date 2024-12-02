namespace unifor_aep.Models
{
    public class RecentlyItemLost
    {
        public int id { get; set; }
        public int id_user { get; set; }
        public string description { get; set; }
        public string place_lost { get; set; }
        public string date { get; set; }
    }
}
