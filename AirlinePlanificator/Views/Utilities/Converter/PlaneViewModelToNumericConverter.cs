using AirlinePlanificator.ViewModels;
using System;
using System.Globalization;
using System.Windows.Data;

namespace AirlinePlanificator.Views.Utilities.Converter
{
    public class PlaneViewModelToNumericConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PlaneViewModel)
            {
                var rec = (PlaneViewModel)value;
                return rec.Category;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PlaneViewModel)
            {
                var rec = (PlaneViewModel)value;
                return rec.Category;
            }

            return value;
        }
    }
}
