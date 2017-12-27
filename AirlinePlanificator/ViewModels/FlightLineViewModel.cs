using AirlinePlanificator.ViewModels.Utilities;
using AirlinePlanificator.Utilities;

namespace AirlinePlanificator.ViewModels
{
    public class FlightLineViewModel : ObservableObject
    {
        #region Members
        private AirportViewModel _departureAirport;
        private AirportViewModel _arrivalAirport;
        private AirportSelectorViewModel _airportSelector;
        #endregion

        #region Properties
        public AirportViewModel DepartureAirport
        {
            get
            {
                return _departureAirport;
            }
            set
            {
                _departureAirport = value;
            }
        }
        public AirportViewModel ArrivalAirport
        {
            get
            {
                return _arrivalAirport;
            }
            set
            {
                _arrivalAirport = value;
            }
        }
        public PlaneViewModel PlaneIndicator { get { return _airportSelector.PlaneIndicator; } }
        public AirportSelectorViewModel OwnerAirportSelector
        {
            get { return _airportSelector; }
        }
        public double FlightDistance
        {
            get
            {
                double distance = 0;
                if (CalculateFlightDistanceCommand.CanExecute(null))
                {
                    distance = CalculateFlightDistanceCommand.Execute(null);
                }
                return distance;
            }
        }
        public double FlightTime
        {
            get
            {
                if (CalculateFlightTimeCommand.CanExecute(null))
                {
                    return CalculateFlightTimeCommand.Execute(null);
                }
                return 0;
            }
        }
        public string Name
        {
            get { return string.Format("[{0}] => [{1}]", DepartureAirport, ArrivalAirport); }
        }
        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region Construction
        public FlightLineViewModel(AirportSelectorViewModel airportSelector, AirportViewModel departureAirport, AirportViewModel arrivalAirport)
        {
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            _airportSelector = airportSelector;
            _airportSelector.PropertyChanged += PlaneIndicator_PropertyChanged;
        }
        #endregion

        #region Events
        void PlaneIndicator_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            string PlaneIndicatorPropertyName = ReflectorPropertyName.GetPropertyName<AirportSelectorViewModel>((a) => a.PlaneIndicator);
            string FlightTimePropertyName = ReflectorPropertyName.GetPropertyName(() => FlightTime);

            if (e.PropertyName == PlaneIndicatorPropertyName)
                RaisePropertyChanged(FlightTimePropertyName);
        }
        #endregion

        #region Commands

        #region CalculateFlightDistance
        private double CalculateFlightDistanceCommandExecute(object parameter)
        {
             double distance = FlightCalculator.FlightDistance(
                DepartureAirport.Latitude.Value,
                DepartureAirport.Longitude.Value,
                ArrivalAirport.Latitude.Value,
                ArrivalAirport.Longitude.Value);
            return distance;
        }
        bool CanCalculateFlightDistanceCommandExecute(object parameter)
        {
            bool canExecute = DepartureAirport != null && ArrivalAirport != null &&
                DepartureAirport.Latitude.HasValue &&
                DepartureAirport.Longitude.HasValue &&
                ArrivalAirport.Latitude.HasValue &&
                ArrivalAirport.Longitude.HasValue;
            return canExecute;
        }
        public RelayCommand<object, double> CalculateFlightDistanceCommand { get { return new RelayCommand<object, double>(CalculateFlightDistanceCommandExecute, CanCalculateFlightDistanceCommandExecute); } }
        #endregion


        #region CalculateFlightTime
        private double CalculateFlightTimeCommandExecute(object parameter)
        {
            double time = FlightCalculator.FlightTime(FlightDistance, PlaneIndicator);
            return time;
        }
        bool CanCalculateFlightTimeCommandExecute(object parameter)
        {
            bool canExecute = CanCalculateFlightDistanceCommandExecute(null) &&
                PlaneIndicator != null && PlaneIndicator.Speed > 0;
            return canExecute;
        }
        public RelayCommand<object, double> CalculateFlightTimeCommand { get { return new RelayCommand<object, double>(CalculateFlightTimeCommandExecute, CanCalculateFlightTimeCommandExecute); } }
        #endregion
                                                                                                                                  
        #endregion
    }
}
