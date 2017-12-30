using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AirlinePlanificator.ViewModels.Utilities;

namespace AirlinePlanificator.Utilities
{

    public class DemandClass : ObservableObject, IDictionary<PassengerClassType, PassengerClass>
    {
        private Dictionary<PassengerClassType, PassengerClass> ClassDemand { get; set; }

        public DemandClass()
        {
            ClassDemand = new Dictionary<PassengerClassType, PassengerClass>(3);
            ClassDemand[PassengerClassType.Business] = new PassengerClass(PassengerClassType.Business);
            ClassDemand[PassengerClassType.Economic] = new PassengerClass(PassengerClassType.Economic);
            ClassDemand[PassengerClassType.First] = new PassengerClass(PassengerClassType.First);

            ClassDemand[PassengerClassType.Business].PropertyChanged += OnDemandPropertyChanged;
            ClassDemand[PassengerClassType.Economic].PropertyChanged += OnDemandPropertyChanged;
            ClassDemand[PassengerClassType.First].PropertyChanged += OnDemandPropertyChanged;
        }

        private void OnDemandPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            RaisePropertyChanged(() => TotalPax);
        }

        public double TotalPax
        {
            get
            {
                return Math.Round(this.Sum(p => p.Value.Passenger * p.Value.ClassPaxValue), 0);
            }
        }

        #region IDictionary<PassengerClassType, PassengerClass>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyValuePair<PassengerClassType, PassengerClass>> GetEnumerator()
        {
            return ClassDemand.GetEnumerator();
        }

        void ICollection<KeyValuePair<PassengerClassType, PassengerClass>>.Add(KeyValuePair<PassengerClassType, PassengerClass> item)
        {
            ClassDemand.Add(item.Key, item.Value);
        }

        void ICollection<KeyValuePair<PassengerClassType, PassengerClass>>.Clear()
        {
            ClassDemand.Clear();
        }

        public bool Contains(KeyValuePair<PassengerClassType, PassengerClass> item)
        {
            return ClassDemand.Contains(item);
        }

        void ICollection<KeyValuePair<PassengerClassType, PassengerClass>>.CopyTo(KeyValuePair<PassengerClassType, PassengerClass>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        bool ICollection<KeyValuePair<PassengerClassType, PassengerClass>>.Remove(KeyValuePair<PassengerClassType, PassengerClass> item)
        {
            return ClassDemand.Remove(item.Key);
        }

        public int Count
        {
            get { return ClassDemand.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool ContainsKey(PassengerClassType key)
        {
            return ClassDemand.ContainsKey(key);
        }

        void IDictionary<PassengerClassType, PassengerClass>.Add(PassengerClassType key, PassengerClass value)
        {
            ClassDemand.Add(key, value);
        }

        bool IDictionary<PassengerClassType, PassengerClass>.Remove(PassengerClassType key)
        {
            return ClassDemand.Remove(key);
        }

        public bool TryGetValue(PassengerClassType key, out PassengerClass value)
        {
            return ClassDemand.TryGetValue(key, out value);
        }

        public ICollection<PassengerClassType> Keys
        {
            get { return ClassDemand.Keys; }
        }

        public ICollection<PassengerClass> Values
        {
            get
            {
                return ClassDemand.Values;
            }
        }

        public PassengerClass this[PassengerClassType key]
        {
            get { return ClassDemand[key]; }
            set
            {
                ClassDemand[key] = value;
                RaisePropertyChanged(() => TotalPax);
            }
        }

        #endregion
    }
}
