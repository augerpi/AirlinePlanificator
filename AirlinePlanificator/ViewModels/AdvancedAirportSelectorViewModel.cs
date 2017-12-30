using System;
using AirlinePlanificator.ViewModels.Utilities;
using System.Collections.Generic;
using System.Globalization;
using AirlinePlanificator.Utilities;
using System.Linq;
using AirlinePlanificator.Views.Utilities.Converter;

namespace AirlinePlanificator.ViewModels
{
    public class AdvancedAirportSelectorViewModel : ObservableObject, IAirportSelectorViewModel
    {
        #region Members
        private readonly AirportSelectorViewModel _baseViewModel;
        private FlightLineViewModel _lineIndicator;
        private Dictionary<Int32, String> _distanceAvailable;
        #endregion

        #region Properties
        public FlightLineViewModel LineIndicator
        {
            get { return _lineIndicator; }
            set
            {
                if (_lineIndicator != value)
                {
                    _lineIndicator = value;
                    RaisePropertyChanged();
                }
            }
        }

        public KeyValuePair<Int32, String>? DistanceMin { get; set; }
        public KeyValuePair<Int32, String>? DistanceMax { get; set; }

        public Dictionary<Int32, String> DistanceAvailable
        {
            get
            {
                return _distanceAvailable;
            }
            private set
            {
                _distanceAvailable = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        public AdvancedAirportSelectorViewModel(AirportSelectorViewModel baseViewModel)
        {
            _baseViewModel = baseViewModel;
            _baseViewModel.PropertyChanged += (sender, args) =>
            {
                if (Equals(args.PropertyName, ReflectorPropertyName.GetPropertyName<AirportSelectorViewModel>(model => model.PlaneIndicator)))
                {
                    RefreshAvailableDistanceIndicator();
                }
            };
        }

        private void RefreshAvailableDistanceIndicator()
        {
            if (_baseViewModel.PlaneIndicator != null)
            {
                doubleToHour displayHourConverter = new doubleToHour();
                Int32 departureAndArrivalDelay = 2;
                //Generate 2h to 48h with 15m steps
                DistanceAvailable = Enumerable.Range(0, (48 - departureAndArrivalDelay) * 4 + 1)
                    .Select(i => i / 4.0 + departureAndArrivalDelay)
                    .ToDictionary(hour => Convert.ToInt32(Math.Ceiling(FlightCalculator.DistanceFromFlightTime(hour, _baseViewModel.PlaneIndicator))), 
                                  hour => (String)displayHourConverter.Convert(hour, typeof(String), null, CultureInfo.InvariantCulture));
            }
            else
            {
                DistanceAvailable = new Dictionary<Int32, String>();
            }
        }

        #region IAirportSelectorViewModel implementation

        public HubViewModel DepartureHub
        {
            get { return _baseViewModel.DepartureHub; }
        }

        public CompanyViewModel Company
        {
            get { return _baseViewModel.Company; }
        }

        public PlaneViewModel PlaneIndicator
        { 
            get { return _baseViewModel.PlaneIndicator; }
            set { _baseViewModel.PlaneIndicator = value; }
        }
        public FlightLineViewModel SelectedFlightLine
        {
            get { return _baseViewModel.SelectedFlightLine; }
            set { _baseViewModel.SelectedFlightLine = value; }
        }
        public List<PlaneViewModel> AvailablePlanes
        {
            get { return _baseViewModel.AvailablePlanes; }
        }

        public List<FlightLineViewModel> AvailableFlightLines
        {
            get { return _baseViewModel.AvailableFlightLines; }
        }

        public List<string> AvailableCountries
        {
            get { return _baseViewModel.AvailableCountries; }
        }

        #endregion
    }
}
