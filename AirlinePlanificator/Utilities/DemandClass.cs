using System;
using System.Collections.Generic;
using System.Linq;

namespace AirlinePlanificator.Utilities
{

    public class DemandClass : Dictionary<PassengerClassType, PassengerClass>
    {
        public DemandClass()
            : base(3)
        {
            base[PassengerClassType.Business] = new PassengerClass(PassengerClassType.Business);
            base[PassengerClassType.Economic] = new PassengerClass(PassengerClassType.Economic);
            base[PassengerClassType.First] = new PassengerClass(PassengerClassType.First);
        }

        public double TotalPax
        {
            get
            {
                return Math.Round(this.Sum(p => p.Value.Passenger * p.Value.ClassPaxValue), 0);
            }
        }

        public PassengerClass this[PassengerClassType classType]
        {
            get { return base[classType]; }
            protected set { base[classType] = value; }
        }
    }
}
