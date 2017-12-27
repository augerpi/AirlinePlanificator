using System.Collections.Generic;
using AirlinePlanificator.ViewModels.FlightPlanifications.PlanificationResults;

namespace AirlinePlanificator.ViewModels.FlightPlanifications
{
    public abstract class PlanificationStrategy
    {
        public FlightPlanificatorViewModel FlightPlanificator { get; set; }

        protected PlanificationStrategy(FlightPlanificatorViewModel flightPlanificator)
        {
            FlightPlanificator = flightPlanificator;
        }

        public abstract List<PlaneConfigurationResult> Execute();
    }
}
