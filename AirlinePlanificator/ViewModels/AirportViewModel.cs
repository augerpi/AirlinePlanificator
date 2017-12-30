//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

using AirlinePlanificator.Models;
using AirlinePlanificator.ViewModels.Utilities;

namespace AirlinePlanificator.ViewModels
{
    public class AirportViewModel : ObservableObject
    {
        #region Members
        Airport _airport;
        #endregion

        #region Properties
        public Airport Airport
        {
            get
            {
                return _airport;
            }
            set
            {
                _airport = value;
            }
        }
        public int Category
        {
            get { return Airport.Category; }
            set
            {
                if (Airport.Category != value)
                {
                    Airport.Category = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string City
        {
            get { return Airport.City; }
            set
            {
                if (Airport.City != value)
                {
                    Airport.City = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string CodeIATA
        {
            get { return Airport.CodeIATA; }
            set
            {
                if (Airport.CodeIATA != value)
                {
                    Airport.CodeIATA = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string Country
        {
            get { return Airport.Country; }
            set
            {
                if (Airport.Country != value)
                {
                    Airport.Country = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string Name
        {
            get { return Airport.Name; }
            set
            {
                if (Airport.Name != value)
                {
                    Airport.Name = value;
                    RaisePropertyChanged();
                }
            }
        }
        public double? Latitude { get { return Airport.Latitude; } }
        public double? Longitude { get { return Airport.Longitude; } }
        public decimal BuyPrice
        {
            get
            {
                return Airport.BuyPrice;
            }
            set
            {
                if (Airport.BuyPrice != value)
                {
                    Airport.BuyPrice = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string FlagImage
        {
            get
            {
                return Airport.FlagImage;
            }
            set
            {
                if (Airport.FlagImage != value)
                {
                    Airport.FlagImage = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string CompleteName
        {
            get { return string.Format("{0} {1} - {2}", CodeIATA, Name, Country); }
        }
        public override string ToString()
        {
            return CompleteName;
        }

        #endregion

        #region Construction
        public AirportViewModel(Airport airport)
        {
            Airport = airport;
        }
        #endregion

        #region Commands


        #endregion
    }
}
