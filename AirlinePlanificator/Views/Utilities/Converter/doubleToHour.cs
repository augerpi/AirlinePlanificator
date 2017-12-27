using System;
using System.Windows.Data;

namespace AirlinePlanificator.Views.Utilities.Converter
{
    public class doubleToHour : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                double numericValue = (double)value;

                return string.Format("{0}h{1:00}", (int)numericValue, (numericValue % 1) * 60);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
