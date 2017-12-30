using System;
using System.Collections.Generic;
using System.Linq;
using AirlinePlanificator.ViewModels;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using AirlinePlanificator.Utilities;

namespace AirlinePlanificator.Views.Controls
{
    /// <summary>
    /// Interaction logic for AirportSelector.xaml
    /// </summary>
    public partial class AirportSelector : UserControl
    {
        public AirportSelector()
        {
            InitializeComponent();
        }

        private void cboPlaneIndicator_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AirportSelectorViewModel context = (AirportSelectorViewModel)DataContext;
            RadGridView grid = context.DepartureHub == null ? grid = grdAirportDeparture : grdAirportArrival;
            Telerik.Windows.Controls.GridView.IColumnFilterDescriptor columnFilter;
            Telerik.Windows.Controls.GridViewColumn column;
            string uniqueName;

            grid.FilterDescriptors.SuspendNotifications();

            if (context.PlaneIndicator != null)
            {
                //Category
                string airportByContext = context.DepartureHub == null ? ReflectorPropertyName.GetPropertyName<FlightLineViewModel>((o) => o.DepartureAirport) : ReflectorPropertyName.GetPropertyName<FlightLineViewModel>((o) => o.ArrivalAirport);
                uniqueName = string.Format("{0}.{1}", airportByContext, ReflectorPropertyName.GetPropertyName<AirportViewModel>((o) => o.Category));
                columnFilter = grid.Columns[uniqueName].ColumnFilterDescriptor;
                columnFilter.FieldFilter.Filter1.Operator = FilterOperator.IsGreaterThanOrEqualTo;
                columnFilter.FieldFilter.Filter1.Value = context.PlaneIndicator.Category;
                //Range
                if (context.DepartureHub != null)
                {
                    uniqueName = ReflectorPropertyName.GetPropertyName<FlightLineViewModel>((o) => o.FlightDistance);
                    columnFilter = grid.Columns[uniqueName].ColumnFilterDescriptor;
                    columnFilter.FieldFilter.Filter1.Operator = FilterOperator.IsLessThanOrEqualTo;
                    columnFilter.FieldFilter.Filter1.Value = context.PlaneIndicator.Range;
                }
            }
            else //Clear filter
            {
                //Category
                string airportByContext = context.DepartureHub == null ? ReflectorPropertyName.GetPropertyName<FlightLineViewModel>((o) => o.DepartureAirport) : ReflectorPropertyName.GetPropertyName<FlightLineViewModel>((o) => o.ArrivalAirport);
                uniqueName = string.Format("{0}.{1}", airportByContext, ReflectorPropertyName.GetPropertyName<AirportViewModel>((o) => o.Category));
                column = grid.Columns[uniqueName];
                column.ClearFilters();
                //Range
                if (context.DepartureHub != null)
                {
                    uniqueName = ReflectorPropertyName.GetPropertyName<FlightLineViewModel>((o) => o.FlightDistance);
                    column = grid.Columns[uniqueName];
                    column.ClearFilters();
                }
            }

            grid.FilterDescriptors.ResumeNotifications();
        }
    }
}
