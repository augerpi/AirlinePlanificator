using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using System.Collections.ObjectModel;
using AirlinePlanificator.Models;
using AirlinePlanificator.ViewModels.Utilities;

namespace AirlinePlanificator.ViewModels
{
    public class CompanyViewModel : ObservableObject
    {
        #region Members
        private const string DataFile = "AirlinePlanificatorData.json";

        Company _company;
        HubViewModel _selectedHub;
        ObservableCollection<HubViewModel> _hubs = new ObservableCollection<HubViewModel>();
        ObservableCollection<PlaneViewModel> _allPlanes = new ObservableCollection<PlaneViewModel>();
        ObservableCollection<AirportViewModel> _allAirports = new ObservableCollection<AirportViewModel>();
        #endregion

        #region Properties
        public ObservableCollection<PlaneViewModel> AllPlanes
        {
            get { return _allPlanes; }
            internal set { _allPlanes = value; }
        }
        public ObservableCollection<AirportViewModel> AllAirports
        {
            get { return _allAirports; }
            internal set { _allAirports = value; }
        }
        public ObservableCollection<HubViewModel> Hubs
        {
            get { return _hubs; }
            internal set { _hubs = value; }
        }
        public Company Company
        {
            get
            {
                return _company;
            }
            set
            {
                _company = value;

                SetAirportList(value.Airports);
                SetPlaneList(value.Planes);
                SetHubList(value.Hubs);
            }
        }
        public string CompanyName
        {
            get { return Company.CompanyName; }
            set
            {
                if (Company.CompanyName != value)
                {
                    Company.CompanyName = value;
                    RaisePropertyChanged();
                }
            }
        }

        public HubViewModel SelectedHub
        {
            get 
            {
                if (_selectedHub == null)
                {
                    //Get selectedHub from model or take first hub in the list
                    SelectedHub = Hubs.FirstOrDefault(h => h.DepartureAirport.CodeIATA == Company.SelectedHub);
                    if (_selectedHub == null)
                    {
                        SelectedHub = Hubs.FirstOrDefault();
                    }
                    
                }
                return _selectedHub; 
            }
            set 
            {
                if (_selectedHub != value)
                {
                    _selectedHub = value;
                    string newCode = (value != null ? value.Hub.HubCode : null);
                    if (Company.SelectedHub != newCode)
                    {
                        Company.SelectedHub = newCode;
                        RaisePropertyChanged();
                    }
                }
            }
        }
        #endregion

        #region Construction
        public CompanyViewModel()
        {
            if (LoadCompany.CanExecute(null))
            {
                LoadCompany.Execute(null);
            }
            else
            {
                Company = new Company() { CompanyName = "Nouvelle Compagnie" };
                ImportData();
                if (SaveCompany.CanExecute(null))
                {
                    SaveCompany.Execute(null);
                }
            }
        }
        #endregion

        #region Methods
        private void SetAirportList(List<Airport> airports)
        {
            if (airports != null)
            {
                AllAirports = new ObservableCollection<AirportViewModel>(airports.Select(i => new AirportViewModel(i)));
            }
        }
        private void SetPlaneList(List<Plane> planes)
        {
            if (planes != null)
            {
                AllPlanes = new ObservableCollection<PlaneViewModel>(planes.Select(i => new PlaneViewModel(i)));
            }
        }
        private void SetHubList(List<Hub> hubs)
        {
            if (hubs != null) 
            {
                Hubs = new ObservableCollection<HubViewModel>(hubs.Select(i => new HubViewModel(this, i)));
            }
        }
        private void ImportData()
        {
            Import.AirportImportManager importManager = new Import.AirportImportManager();
            List<Airport> airports = importManager.Import();
            Company.Airports = airports;
            SetAirportList(Company.Airports);
        }
        #endregion

        #region Commands
        #region LoadCompany
        void LoadCompanyExecute()
        {
            //Read from file
            string newReadedFile = System.IO.File.ReadAllText(DataFile);
            Company newCompany = Newtonsoft.Json.JsonConvert.DeserializeObject<Company>(newReadedFile);
            Company = newCompany;

            //this.SelectedHub.PlanLineCommand.Execute(null);
        }

        bool CanLoadCompanyExecute()
        {
            return System.IO.File.Exists(DataFile);
        }

        public ICommand LoadCompany { get { return new RelayCommand(LoadCompanyExecute, CanLoadCompanyExecute); } }
        #endregion

        #region SaveCompany
        void SaveCompanyExecute()
        {
            //patch
            Company.Airports = AllAirports.Select(i => i.Airport).ToList();
            Company.Planes = AllPlanes.Select(i => i.Plane).ToList();
            Company.Hubs = Hubs.Select(i => i.GetSavableHub()).ToList();

            //Read from file
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(Company);
            System.IO.File.WriteAllText(DataFile, json);
        }

        bool CanSaveCompanyExecute()
        {
            return Company != null;
        }

        public ICommand SaveCompany { get { return new RelayCommand(SaveCompanyExecute, CanSaveCompanyExecute); } }
        #endregion

        #region AddNewHub
        private void AddNewHubExecute()
        {
            Views.SelectAirport window = new Views.SelectAirport();
            var data = new AirportSelectorViewModel(this, null);
            window.DataContext = data;
            try
            {
                bool? result = window.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    if (data.SelectedFlightLine != null)
                    {
                        HubViewModel newHub = new HubViewModel(this, data.SelectedFlightLine.DepartureAirport.Airport);
                        Hubs.Add(newHub);
                        SelectedHub = newHub;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            
        }
        private bool CanAddNewHubExecute()
        {
            return true;
        }
        public ICommand AddNewHubCommand { get { return new RelayCommand(AddNewHubExecute, CanAddNewHubExecute); } }
        #endregion

        #region DeleteHub
        private void DeleteHubExecute()
        {
            string text = "Voulez-vous vraiment supprimer le hub ainsi que toutes les lignes qui lui sont associées ?";
            var result = System.Windows.MessageBox.Show(text, "Confirmation", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question, System.Windows.MessageBoxResult.No);
            if (result == System.Windows.MessageBoxResult.Yes)
            {
                Hubs.Remove(SelectedHub);
                SelectedHub = Hubs.FirstOrDefault();
            }
        }
        private bool CanDeleteHubExecute()
        {
            return true;
        }
        public ICommand DeleteHubCommand { get { return new RelayCommand(DeleteHubExecute, CanDeleteHubExecute); } }
        #endregion
        #endregion
    }
}
