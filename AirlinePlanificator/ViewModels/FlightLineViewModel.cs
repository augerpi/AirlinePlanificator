using System;
using AirlinePlanificator.ViewModels.Utilities;
using AirlinePlanificator.Utilities;

namespace AirlinePlanificator.ViewModels
{
    public class FlightLineViewModel : ObservableObject, IComparable
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
        public Double FlightDistance
        {
            get
            {
                Double distance = 0;
                if (CalculateFlightDistanceCommand.CanExecute(null))
                {
                    distance = CalculateFlightDistanceCommand.Execute(null);
                }
                return distance;
            }
        }
        public Double FlightTime
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
        public String Name
        {
            get { return String.Format("[{0}] => [{1}]", DepartureAirport, ArrivalAirport); }
        }
        public override String ToString()
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
        void PlaneIndicator_PropertyChanged(Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            String PlaneIndicatorPropertyName = ReflectorPropertyName.GetPropertyName<AirportSelectorViewModel>((a) => a.PlaneIndicator);
            String FlightTimePropertyName = ReflectorPropertyName.GetPropertyName(() => FlightTime);

            if (e.PropertyName == PlaneIndicatorPropertyName)
                RaisePropertyChanged(FlightTimePropertyName);
        }
        #endregion

        #region Commands

        #region CalculateFlightDistance
        private Double CalculateFlightDistanceCommandExecute(Object parameter)
        {
             Double distance = FlightCalculator.FlightDistance(
                DepartureAirport.Latitude.Value,
                DepartureAirport.Longitude.Value,
                ArrivalAirport.Latitude.Value,
                ArrivalAirport.Longitude.Value);
            return distance;
        }
        Boolean CanCalculateFlightDistanceCommandExecute(Object parameter)
        {
            Boolean canExecute = DepartureAirport != null && ArrivalAirport != null &&
                DepartureAirport.Latitude.HasValue &&
                DepartureAirport.Longitude.HasValue &&
                ArrivalAirport.Latitude.HasValue &&
                ArrivalAirport.Longitude.HasValue;
            return canExecute;
        }
        public RelayCommand<Object, Double> CalculateFlightDistanceCommand { get { return new RelayCommand<Object, Double>(CalculateFlightDistanceCommandExecute, CanCalculateFlightDistanceCommandExecute); } }
        #endregion


        #region CalculateFlightTime
        private Double CalculateFlightTimeCommandExecute(Object parameter)
        {
            Double time = FlightCalculator.FlightTimeFromDistance(FlightDistance, PlaneIndicator);
            return time;
        }
        Boolean CanCalculateFlightTimeCommandExecute(Object parameter)
        {
            Boolean canExecute = CanCalculateFlightDistanceCommandExecute(null) &&
                PlaneIndicator != null && PlaneIndicator.Speed > 0;
            return canExecute;
        }
        public RelayCommand<Object, Double> CalculateFlightTimeCommand { get { return new RelayCommand<Object, Double>(CalculateFlightTimeCommandExecute, CanCalculateFlightTimeCommandExecute); } }
        
        #endregion
                                                                                                                                  
        #endregion


        public Int32 CompareTo(Object obj)
        {
            if (obj.GetType() != GetType()) return -1;
            return String.Compare(Name, ((FlightLineViewModel)obj).Name, StringComparison.InvariantCulture);
        }

        protected Boolean Equals(FlightLineViewModel other)
        {
            return Equals(_departureAirport, other._departureAirport) && Equals(_arrivalAirport, other._arrivalAirport);
        }

        public override Boolean Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((FlightLineViewModel)obj);
        }

        public override Int32 GetHashCode()
        {
            unchecked
            {
                return ((_departureAirport != null ? _departureAirport.GetHashCode() : 0) * 397) ^ (_arrivalAirport != null ? _arrivalAirport.GetHashCode() : 0);
            }
        }

        public static Boolean operator ==(FlightLineViewModel left, FlightLineViewModel right)
        {
            return Equals(left, right);
        }

        public static Boolean operator !=(FlightLineViewModel left, FlightLineViewModel right)
        {
            return !Equals(left, right);
        }
    }
}
