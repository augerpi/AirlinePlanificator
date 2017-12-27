using System;
using System.Collections.Generic;
using System.Linq;
using AirlinePlanificator.ViewModels.FlightPlanifications.PlanificationResults;

namespace AirlinePlanificator.ViewModels.FlightPlanifications.PlanificationStrategies
{
    public class SimplePlanification : PlanificationStrategy
    {
        public SimplePlanification(FlightPlanificatorViewModel flightPlanificator) 
            : base(flightPlanificator)
        {
        }

        public override List<PlaneConfigurationResult> Execute()
        {
            Dictionary<PlaneViewModel, Dictionary<Int32, Int32>> paxPerPlane = new Dictionary<PlaneViewModel, Dictionary<int, int>>(FlightPlanificator.PlaneConfigurationList.Count);
            foreach (PlaneConfiguration configuration in FlightPlanificator.PlaneConfigurationList)
            {
                paxPerPlane[configuration.Plane] = Configure(configuration);
            }
            Int32 smallestCapacity = FlightPlanificator.PlaneConfigurationList.Min(c => c.Plane.Capacity);
            List<PlaneConfigurationResult> configListResults = paxPerPlane.Aggregate(new List<PlaneConfigurationResult>(), (container, config) => MatrixEnumerateConfig(container, config, smallestCapacity));
            return configListResults
                .Where(o => FlightPlanificator.ShowAllConfigurations || o.RemainingAbsolutePax <= smallestCapacity)
                .OrderBy(o => o.RemainingAbsolutePax)
                .ToList();
        }

        private List<PlaneConfigurationResult> MatrixEnumerateConfig(List<PlaneConfigurationResult> container, KeyValuePair<PlaneViewModel, Dictionary<int, int>> config, int smallestPlaneCapacity)
        {
            List<PlaneConfigurationResult> containerList = new List<PlaneConfigurationResult>();
            foreach (KeyValuePair<int, int> newPlaneConfig in config.Value)
            {
                IEnumerable<PlaneConfigurationResult> list = null;
                if (container.Any())
                {
                    list = container
                        //When the Pax is exceeding significantly
                        .Where(c => FlightPlanificator.ShowAllConfigurations || (
                            c.Pax < FlightPlanificator.PassengerDemand.TotalPax &&
                            c.Pax + newPlaneConfig.Value < FlightPlanificator.PassengerDemand.TotalPax + smallestPlaneCapacity))
                        //When the Pax is not empty
                        .Where(c =>
                            newPlaneConfig.Value > 0)
                        .ToList()
                        .Select(c =>
                        {
                            PlaneConfigurationResult newList = new PlaneConfigurationResult(Convert.ToInt32(Math.Floor(FlightPlanificator.PassengerDemand.TotalPax)))
                            {
                                PlanesConfig = new List<ConfigInformation>(c.PlanesConfig)
                            };
                            newList.PlanesConfig.Add(new ConfigInformation { Plane = config.Key, Number = newPlaneConfig.Key });
                            return newList;
                        });
                }
                else
                {
                    list = new List<PlaneConfigurationResult>
                    {
                        new PlaneConfigurationResult(Convert.ToInt32(Math.Floor(FlightPlanificator.PassengerDemand.TotalPax)))
                        {
                            PlanesConfig =
                                new List<ConfigInformation>
                                {
                                    new ConfigInformation {Plane = config.Key, Number = newPlaneConfig.Key}
                                }
                        }
                    };
                }
                containerList.AddRange(list);
            }
            container.AddRange(containerList);
            return container;
        }

        private Dictionary<int, int> Configure(PlaneConfiguration configuration)
        {
            PlaneViewModel plane = configuration.Plane;
            Dictionary<int, int> config = Enumerable.Range(0, Convert.ToInt32(Math.Ceiling(FlightPlanificator.PassengerDemand.TotalPax / plane.Capacity / 2)))
                .ToDictionary(i => i, i => i * plane.Capacity * 2);
            return config;
        }
    }
}
