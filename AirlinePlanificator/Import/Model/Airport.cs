namespace AirlinePlanificator.Import.Model
{
    public class Airport
    {
        public string code { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public object terminal { get; set; }
        public object gate { get; set; }
        public string timezone { get; set; }
    }
}
