using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using AirlinePlanificator.Models;
using AirlinePlanificator.ViewModels.Utilities;

namespace AirlinePlanificator.ViewModels
{
    public class HubViewModel : ObservableObject
    {
        
        #region Members
        Hub _hub;
        AirportViewModel _originAirport;
        CompanyViewModel _company;
        ObservableCollection<AirportViewModel> _lines = new ObservableCollection<AirportViewModel>();
        #endregion

        #region Properties
        public Hub Hub
        {
            get
            {
                return _hub;
            }
            set
            {
                _hub = value;
            }
        }
        public ObservableCollection<AirportViewModel> Lines
        {
            get { return _lines; }
            internal set { _lines = value; }
        }
        public AirportViewModel DepartureAirport
        {
            get { return _originAirport; }
            protected set
            {
                if (_originAirport != value)
                {
                    _originAirport = value;
                    RaisePropertyChanged();
                }
            }
        }
        public CompanyViewModel Company
        {
            get { return _company; }
            set
            {
                if (_company != value)
                {
                    _company = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string CompleteHubName
        {
            get 
            {
                if (DepartureAirport != null)
                {
                    return DepartureAirport.CompleteName;
                }
                return string.Empty;
            }
        }
        #endregion

        #region Construction
        public HubViewModel(CompanyViewModel company, Hub hub)
        {
            Hub = hub;
            Company = company;

            DepartureAirport = Company.AllAirports.Single(i => i.CodeIATA == hub.HubCode);
            if (hub.LinesCode != null)
            {
                Lines = new ObservableCollection<AirportViewModel>(Company.AllAirports.Where(i => hub.LinesCode.Contains(i.CodeIATA)));
            }

            //TODO delete for test purpose
            //Random rdn = new Random(DateTime.Now.Millisecond);
            //for (int i = 0; i < 2; i++)
            //{
            //    this.Lines.Add(new AirportViewModel(this.Company.AllAirports.ElementAt(rdn.Next(0, this.Company.AllAirports.Count)).Airport));
            //}
        }
        public HubViewModel(CompanyViewModel company, Airport airport)
        {
            Hub = new Hub() { HubCode = airport.CodeIATA };
            Company = company;
            DepartureAirport = new AirportViewModel(airport);
        }
        #endregion

        #region Methodes
        internal Hub GetSavableHub()
        {
            Hub.LinesCode = Lines.Select(i => i.CodeIATA).ToList();
            return Hub;
        }

        #endregion

        #region Commands
        private void PlanLineExecute()
        {
            Views.LinePlanificator window = new Views.LinePlanificator();
            var data = new FlightPlanificatorViewModel(Company, this);

            //TODO remove this. For test purpose
            data.PassengerDemand[AirlinePlanificator.Utilities.PassengerClassType.Business].Passenger = 153;
            data.PassengerDemand[AirlinePlanificator.Utilities.PassengerClassType.Economic].Passenger = 1562;
            data.PassengerDemand[AirlinePlanificator.Utilities.PassengerClassType.First].Passenger = 115;
            data.PlaneConfigurationList.Add(new FlightPlanifications.PlaneConfiguration() { Plane = data.AirportSelectorViewModel.AvailablePlanes.ElementAt(1) });
            data.PlaneConfigurationList.Add(new FlightPlanifications.PlaneConfiguration() { Plane = data.AirportSelectorViewModel.AvailablePlanes.ElementAt(2) });

            window.DataContext = data;
            bool? result = window.ShowDialog();
            if (result.HasValue && result.Value)
            {
                if (data.AirportSelectorViewModel.SelectedFlightLine != null)
                {
                    Lines.Add(data.AirportSelectorViewModel.SelectedFlightLine.ArrivalAirport);
                }
            }
        }
        private bool CanPlanLineExecute()
        {
            return DepartureAirport != null;
        }
        public ICommand PlanLineCommand { get { return new RelayCommand(PlanLineExecute, CanPlanLineExecute); } }

        #endregion
    }
}
