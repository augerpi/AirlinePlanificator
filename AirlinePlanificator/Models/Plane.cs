namespace AirlinePlanificator.Models
{
    public class Plane
    {
        public string Modele { get; set; }
        public string Constructor { get; set; }
        public bool IsAvailable { get; set; }
        public int Category { get; set; }
        public int Range { get; set; }
        public double Consumption { get; set; }
        public int Speed { get; set; }
        public int Capacity { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal RentPrice { get; set; }
        public decimal RentCautionPrice { get; set; }
        public string RentCompany { get; set; }
        public int Inauguration { get; set; }
    }
}
