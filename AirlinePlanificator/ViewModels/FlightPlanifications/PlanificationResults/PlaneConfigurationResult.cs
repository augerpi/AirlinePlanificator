using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlinePlanificator.ViewModels.FlightPlanifications.PlanificationResults
{
    public class PlaneConfigurationResult
    {
        public List<ConfigInformation> PlanesConfig { get; set; }

        public Int32 TargetPax { get; set; }

        public Int32 Pax
        {
            get { return PlanesConfig.Sum(config => config.Pax); }
        }

        public Int32 RemainingPax
        {
            get { return TargetPax - Pax; }
        }

        public Int32 RemainingAbsolutePax
        {
            get { return Math.Abs(TargetPax - Pax); }
        }

        public PlaneConfigurationResult(Int32 targetPax)
        {
            TargetPax = targetPax;
            PlanesConfig = new List<ConfigInformation>();
        }

        public String Summary
        {
            get
            {
                return String.Join(", ", PlanesConfig.Select(c => String.Format("{0} : {1}", c.Plane.CompleteName, c.Number)));
            }
        }

        public override string ToString()
        {
            return Summary;
        }
    }
}
