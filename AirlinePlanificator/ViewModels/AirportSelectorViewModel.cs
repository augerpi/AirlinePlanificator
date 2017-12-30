using AirlinePlanificator.ViewModels.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace AirlinePlanificator.ViewModels
{
    public interface IAirportSelectorViewModel
    {
        HubViewModel DepartureHub { get; }
        CompanyViewModel Company { get; }
        PlaneViewModel PlaneIndicator { get; set; }
        FlightLineViewModel SelectedFlightLine { get; set; }
        List<PlaneViewModel> AvailablePlanes { get; }
        List<FlightLineViewModel> AvailableFlightLines{ get; }
        List<string> AvailableCountries { get; }
    }

    public class AirportSelectorViewModel : ObservableObject, IAirportSelectorViewModel
    {
        #region Members
        private List<PlaneViewModel> _availablePlane;
        private List<FlightLineViewModel> _availableFlightLines;
        private List<string> _availableCountries;
        private PlaneViewModel _planeIndicator;
        private FlightLineViewModel _selectedFlightLine;
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

        public FlightLineViewModel SelectedFlightLine
        {
            get { return _selectedFlightLine; }
            set
            {
                _selectedFlightLine = value;
                RaisePropertyChanged();
            }
        }

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

        #region Commands
        #region AdvancedAirportSelector
        void AdvancedAirportSelectorExecute()
        {
            Views.AdvancedAirportSelector window = new Views.AdvancedAirportSelector
            {
                DataContext = new AdvancedAirportSelectorViewModel(this)
            };
            bool? result = window.ShowDialog();
            if (result.HasValue && result.Value)
            {

            }
        }

        bool CanAdvancedAirportSelectorExecute()
        {
            return DepartureHub != null;
        }

        public ICommand AdvancedAirportSelector { get { return new RelayCommand(AdvancedAirportSelectorExecute, CanAdvancedAirportSelectorExecute); } }
        #endregion
        #endregion Commands
    }
}
