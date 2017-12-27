using AirlinePlanificator.Models;
using AirlinePlanificator.ViewModels.Utilities;

namespace AirlinePlanificator.ViewModels
{
    public class PlaneViewModel : ObservableObject
    {
        #region Members
        Plane _plane;
        #endregion

        #region Properties
        public Plane Plane
        {
            get
            {
                return _plane;
            }
            set
            {
                _plane = value;
            }
        }
        public decimal BuyPrice
        {
            get { return Plane.BuyPrice; }
            set
            {
                if (Plane.BuyPrice != value)
                {
                    Plane.BuyPrice = value;
                    RaisePropertyChanged();
                }
            }
        }
        public decimal RentCautionPrice
        {
            get { return Plane.RentCautionPrice; }
            set
            {
                if (Plane.RentCautionPrice != value)
                {
                    Plane.RentCautionPrice = value;
                    RaisePropertyChanged();
                }
            }
        }
        public decimal RentPrice
        {
            get { return Plane.RentPrice; }
            set
            {
                if (Plane.RentPrice != value)
                {
                    Plane.RentPrice = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string RentCompany
        {
            get { return Plane.RentCompany; }
            set
            {
                if (Plane.RentCompany != value)
                {
                    Plane.RentCompany = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string Constructor
        {
            get { return Plane.Constructor; }
            set
            {
                if (Plane.Constructor != value)
                {
                    Plane.Constructor = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string Modele
        {
            get { return Plane.Modele; }
            set
            {
                if (Plane.Modele != value)
                {
                    Plane.Modele = value;
                    RaisePropertyChanged();
                }
            }
        }
        public int Capacity
        {
            get { return Plane.Capacity; }
            set
            {
                if (Plane.Capacity != value)
                {
                    Plane.Capacity = value;
                    RaisePropertyChanged();
                }
            }
        }
        public int Category
        {
            get { return Plane.Category; }
            set
            {
                if (Plane.Category != value)
                {
                    Plane.Category = value;
                    RaisePropertyChanged();
                }
            }
        }
        public double Consumption
        {
            get { return Plane.Consumption; }
            set
            {
                if (Plane.Consumption != value)
                {
                    Plane.Consumption = value;
                    RaisePropertyChanged();
                }
            }
        }
        public int Range
        {
            get { return Plane.Range; }
            set
            {
                if (Plane.Range != value)
                {
                    Plane.Range = value;
                    RaisePropertyChanged();
                }
            }
        }
        public int Speed
        {
            get { return Plane.Speed; }
            set
            {
                if (Plane.Speed != value)
                {
                    Plane.Speed = value;
                    RaisePropertyChanged();
                }
            }
        }
        public int Inauguration
        {
            get { return Plane.Inauguration; }
            set
            {
                if (Plane.Inauguration != value)
                {
                    Plane.Inauguration = value;
                    RaisePropertyChanged();
                }
            }
        }
        public bool IsAvailable 
        {
            get { return Plane.IsAvailable; }
            set
            {
                if (Plane.IsAvailable != value)
                {
                    Plane.IsAvailable = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string CompleteName
        {
            get { return string.Format("{0} - {1}", Constructor, Modele); }
        }

        public override string ToString()
        {
            return CompleteName;
        }
        #endregion

        #region Construction
        public PlaneViewModel(Plane plane)
        {
            Plane = plane;
        }
        #endregion

        #region Commands
        //void CommandExecute()
        //{
            
        //}

        //bool CanCommandExecute()
        //{
        //    return true;
        //}

        //public ICommand Command { get { return new RelayCommand(CommandExecute, CanCommandExecute); } }

        #endregion
    }
}
