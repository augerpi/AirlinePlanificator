using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AirlinePlanificator.ViewModels.Utilities;

namespace AirlinePlanificator.ViewModels.FlightPlanifications
{
    public class PlaneConfigurationList : ObservableCollection<PlaneConfiguration>
    {
        private Func<List<PlaneViewModel>> GetAvailablePlanes { get; set; }

        public List<PlaneViewModel> AvailablePlanes 
        {
            get { return GetAvailablePlanes(); }
        }

        public PlaneConfigurationList(Func<List<PlaneViewModel>> getAvailablePlanes)
        {
            GetAvailablePlanes = getAvailablePlanes;
        }

        #region Commands
        #region AddNewPlaneConfiguration
        private void AddNewPlaneConfigurationExecute()
        {
            Add(new PlaneConfiguration());
        }
        private bool CanAddNewPlaneConfigurationExecute()
        {
            return true;
        }
        public ICommand AddNewPlaneConfigurationCommand { get { return new RelayCommand(AddNewPlaneConfigurationExecute, CanAddNewPlaneConfigurationExecute); } }
        #endregion

        #region RemovePlaneConfiguration
        private void RemovePlaneConfigurationExecute()
        {
            RemoveAt(Count - 1);
        }
        private bool CanRemovePlaneConfigurationExecute()
        {
            return Count > 0;
        }
        public ICommand RemovePlaneConfigurationCommand { get { return new RelayCommand(RemovePlaneConfigurationExecute, CanRemovePlaneConfigurationExecute); } }
        #endregion
        #endregion
    }
}
