using System.Collections.Generic;
using AirlinePlanificator.ViewModels;
using System.Windows.Data;
using System.Collections;
using System.ComponentModel;

namespace AirlinePlanificator.Views.Utilities
{
    public static class LiveShaping
    {
        public static void FilteringAirportByPlaneCategory(IEnumerable<FlightLineViewModel> ItemsSource, int planeCategory)
        {
            // Enable the filtering on Category 
            ListCollectionView empView = CollectionViewSource.GetDefaultView(ItemsSource) as ListCollectionView;
            empView.IsLiveFiltering = true;
            empView.LiveFilteringProperties.Add("Category");
            empView.Filter = (item) =>
            {
                FlightLineViewModel flight = item as FlightLineViewModel;
                return (flight != null) && flight.DepartureAirport.Category >= planeCategory;
            };
            empView.Refresh();
        }
        public static void FilteringAirportByCountry(IEnumerable ItemsSource, string country)
        {
            // Enable the filtering on Category 
            ListCollectionView empView = CollectionViewSource.GetDefaultView(ItemsSource) as ListCollectionView;
            empView.IsLiveFiltering = true;
            empView.LiveFilteringProperties.Add("Country");
            empView.Filter = (item) =>
            {
                AirportViewModel airport = item as AirportViewModel;
                return (airport != null) && airport.Country == country;
            };
            empView.Refresh();
        }

        /// <summary>
        /// Sort a collection
        /// </summary>
        /// <param name="ItemsSource">An object reference to the binding source to be sorted</param>
        /// <param name="propertyName">The name of the property to sort the list by.</param>
        public static void SortingAscending(IEnumerable ItemsSource, string propertyName)
        {
            //cboPlaneIndicator Live shaping (sort)  
            ICollectionView pView = CollectionViewSource.GetDefaultView(ItemsSource);
            pView.SortDescriptions.Add(new SortDescription(propertyName, ListSortDirection.Ascending));
            ICollectionViewLiveShaping viewLiveShaping = (ICollectionViewLiveShaping)CollectionViewSource.GetDefaultView(ItemsSource);
            viewLiveShaping.IsLiveSorting = true;
        }
    }
}
