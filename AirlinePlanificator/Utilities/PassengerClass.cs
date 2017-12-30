using AirlinePlanificator.ViewModels.Utilities;

namespace AirlinePlanificator.Utilities
{
    public class PassengerClass : ObservableObject
    {
        private int _passenger;

        public double ClassPaxValue 
        { 
            get 
            {
                switch (ClassType)
                {
                    case PassengerClassType.Economic:
                        return 1.0;
                    case PassengerClassType.Business:
                        return 1.8;
                    case PassengerClassType.First:
                        return 4.2;
                    default:
                        return 0;
                } 
            } 
        }

        public PassengerClassType ClassType { get; protected set; }

        public int Passenger
        {
            get { return _passenger; }
            set
            {
                _passenger = value; 
                RaisePropertyChanged();
            }
        }

        public PassengerClass(PassengerClassType classType)
        {
            ClassType = classType;
        }
    }
}
