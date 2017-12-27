using System;
using System.Windows.Data;

namespace AirlinePlanificator.Views.Utilities.Converter
{
    public class DoubleToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
                return value.ToString();
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double num;
            if (value != null && Double.TryParse(value.ToString(),out num))
                return num;
            else
                return null;
        }
    }
}
