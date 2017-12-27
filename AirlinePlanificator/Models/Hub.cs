using System.Collections.Generic;

namespace AirlinePlanificator.Models
{
    public class Hub
    {
        public string HubCode { get; set; }
        public List<string> LinesCode { get; set; }
    }
}
