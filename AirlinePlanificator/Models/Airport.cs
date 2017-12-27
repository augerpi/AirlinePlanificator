namespace AirlinePlanificator.Models
{
    public class Airport
    {
        public string CodeIATA { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int Category { get; set; }
        public decimal BuyPrice { get; set; }
        public string FlagImage { get; set; }
    }
}
