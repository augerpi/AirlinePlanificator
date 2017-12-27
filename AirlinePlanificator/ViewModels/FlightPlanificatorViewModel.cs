using System;
using System.Linq;
using AirlinePlanificator.Utilities;
using AirlinePlanificator.ViewModels.FlightPlanifications;
using AirlinePlanificator.ViewModels.FlightPlanifications.PlanificationResults;
using AirlinePlanificator.ViewModels.FlightPlanifications.PlanificationStrategies;
using AirlinePlanificator.ViewModels.Utilities;

namespace AirlinePlanificator.ViewModels
{
    public class FlightPlanificatorViewModel : ObservableObject
    {
        
        #region Members

        #endregion

        #region Properties
        public CompanyViewModel Company { get; private set; }
        public DemandClass PassengerDemand { get; set; }
        public AirportSelectorViewModel AirportSelectorViewModel { get; set; }
        public PlaneConfigurationList PlaneConfigurationList { get; set; }
        public PlaneConfigurationListResult ConfigurationResult { get; set; }
        public Boolean ShowAllConfigurations { get; set; }
        #endregion

        #region Construction
        public FlightPlanificatorViewModel(CompanyViewModel company, HubViewModel hubViewModel)
        {
            Company = company;
            AirportSelectorViewModel = new AirportSelectorViewModel(company, hubViewModel);
            PassengerDemand = new DemandClass();
            PlaneConfigurationList = new PlaneConfigurationList(()=>AirportSelectorViewModel.AvailablePlanes);
            ConfigurationResult = new PlaneConfigurationListResult();
        }
        #endregion

        #region Commands

        #region FlightPlanification
        private void FlightPlanificationCommandExecute()
        {
            PlanificationStrategy planification = new SimplePlanification(this);
            ConfigurationResult.Clear();
            planification.Execute().ForEach(o => ConfigurationResult.Add(o));
        }
        bool CanFlightPlanificationCommandExecute()
        {
            bool canExecute = AirportSelectorViewModel.SelectedFlightLine != null &&
                                AirportSelectorViewModel.SelectedFlightLine.DepartureAirport != null &&
                                AirportSelectorViewModel.SelectedFlightLine.ArrivalAirport != null &&
                                PlaneConfigurationList.Count > 0 && 
                                PlaneConfigurationList.All(config=>config.Plane != null);
            return canExecute;
        }
        public RelayCommand FlightPlanificationCommand { get { return new RelayCommand(FlightPlanificationCommandExecute, CanFlightPlanificationCommandExecute); } }
        #endregion

        #endregion
    }
}
