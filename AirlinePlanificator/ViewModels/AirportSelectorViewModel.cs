using AirlinePlanificator.ViewModels.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace AirlinePlanificator.ViewModels
{
    public class AirportSelectorViewModel : ObservableObject
    {
        #region Members
        protected List<PlaneViewModel> _availablePlane;
        protected List<FlightLineViewModel> _availableFlightLines;
        protected List<string> _availableCountries;
        protected PlaneViewModel _planeIndicator;
        #endregion

        #region Properties
        public HubViewModel DepartureHub { get; private set; }
        public CompanyViewModel Company { get; private set; }
        public PlaneViewModel PlaneIndicator
        {
            get { return _planeIndicator; }
            set
            {
                if (_planeIndicator != value)
                {
                    _planeIndicator = value;
                    RaisePropertyChanged();
                }
            }
        }
        public FlightLineViewModel SelectedFlightLine { get; set; }
        public List<PlaneViewModel> AvailablePlanes
        {
            get
            {
                if (_availablePlane == null && Company != null)
                {
                    var planes = Company.AllPlanes.Where(p => p.IsAvailable);
                    if (DepartureHub != null && DepartureHub.DepartureAirport != null && DepartureHub.DepartureAirport.Category > 0)
                    {
                        planes = planes.Where(p => p.Category <= DepartureHub.DepartureAirport.Category);
                    }
                    _availablePlane = planes.ToList();
                }
                return _availablePlane;
            }
        }
        public List<FlightLineViewModel> AvailableFlightLines
        {
            get
            {
                if (_availableFlightLines == null && Company != null)
                {
                    ////Get available airport by plane Category
                    //    if (this.PlaneIndicator != null)
                    //    {
                    //        _availableAirports = this.Company.AllAirports.Where(p => p.Category >= this.PlaneIndicator.Category).ToList();
                    //    }
                    //    else
                    //    { 

                    if (DepartureHub != null)
                    {
                        _availableFlightLines = Company.AllAirports
                                .Where(a => !DepartureHub.Lines.Contains(a))
                                .Select(a => new FlightLineViewModel(this, DepartureHub.DepartureAirport, a)).ToList();
                    }
                    else
                    {
                        _availableFlightLines = Company.AllAirports
                            .Where(a => !Company.Hubs.Select(h => h.DepartureAirport).Contains(a))
                            .Select(a => new FlightLineViewModel(this, a, null)).ToList();
                    }

                    //    }

                }
                return _availableFlightLines;
            }
        }
        public List<string> AvailableCountries
        {
            get
            {
                if (_availableCountries == null && Company != null)
                {
                    _availableCountries = Company.AllAirports.Select(a => a.Country).Distinct().ToList();
                }
                return _availableCountries;
            }
        }

        #endregion

        #region Construction
        public AirportSelectorViewModel(CompanyViewModel company, HubViewModel hubViewModel)
        {
            DepartureHub = hubViewModel;
            Company = company;
        }

        #endregion


    }
}
