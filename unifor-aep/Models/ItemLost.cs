namespace unifor_aep.Models
{
    public class ItemLost
    {
        public int id { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public bool with_label { get; set; }
        public bool metal { get; set; }
        public bool colored { get; set; }
        public bool broken { get; set; }
        public bool dirty { get; set; }
        public bool opaque { get; set; }
        public bool fragile { get; set; }
        public bool missing_parts { get; set; }
        public bool heavy { get; set; }
        public bool with_pockets { get; set; }
        public bool with_buttons { get; set; }
        public string other { get; set; }
        public int id_user { get; set; }
        public string date { get; set; }
        public string image { get; set; }
    }
}
