using System.Collections.Generic;

namespace AirlinePlanificator.Models
{
    public class Company
    {

        public string CompanyName { get; set; }
        public string SelectedHub { get; set; }
        public List<Hub> Hubs { get; set; }
        public List<Plane> Planes { get; set; }
        public List<Airport> Airports { get; set; }
        
    }
}
