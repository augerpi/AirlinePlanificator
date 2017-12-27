using System.Collections.Generic;

namespace AirlinePlanificator.Import.Model
{
    class ResponseObject
    {
        public int processingDurationMillis { get; set; }
        public bool authorisedAPI { get; set; }
        public bool success { get; set; }
        public object airline { get; set; }
        public object errorMessage { get; set; }
        public List<Airport> airports { get; set; }
    }
}
