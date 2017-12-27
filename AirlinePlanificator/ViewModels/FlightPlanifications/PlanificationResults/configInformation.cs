using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace AirlinePlanificator.ViewModels.FlightPlanifications.PlanificationResults
{
    public class ConfigInformation
    {
        public PlaneViewModel Plane { get; set; }
        public Int32 Number { get; set; }

        public Int32 Pax
        {
            get { return Plane.Capacity * Number * 2; }
        }
    }
}
