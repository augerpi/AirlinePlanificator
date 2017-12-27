using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows;

namespace AirlinePlanificator.Views.Utilities.Converter
{
    public class NumberFormatConverter : System.Windows.Data.IValueConverter
    {
        public string GroupSeperator { get; set; }

        public Type Type { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;

            var type = value.GetType();
            var stringFormat = parameter as string;
            if (IsNumeric(type))
            {
                if (stringFormat == null)
                {
                    return value.ToString();
                }
                var formattible = (IFormattable)value;
                // Gets a NumberFormatInfo associated with the en-US culture.
                NumberFormatInfo nfi;
                if (GroupSeperator == null)
                {
                    nfi = culture.NumberFormat;
                }
                else
                {
                    nfi = ((CultureInfo)culture.Clone()).NumberFormat;
                    nfi.NumberGroupSeparator = GroupSeperator;
                }
                return formattible.ToString(stringFormat, nfi);
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object returnValue;
            if (!IsNumeric(Type))
                return DependencyProperty.UnsetValue;

            NumberFormatInfo nfi;
            if (GroupSeperator == null)
            {
                nfi = culture.NumberFormat;
            }
            else
            {
                nfi = ((CultureInfo)culture.Clone()).NumberFormat;
                nfi.NumberGroupSeparator = GroupSeperator;
            }

            MethodInfo parseMethod = Type.GetMethod("Parse", new Type[] { typeof(string), typeof(NumberStyles), typeof(IFormatProvider) });
            object[] parameters = new object[] 
            { 
                value.ToString(), 
                NumberStyles.Float | NumberStyles.AllowThousands,
                (IFormatProvider)nfi.GetFormat(typeof(NumberFormatInfo)),
            };
            returnValue = parseMethod.Invoke(GetType(), parameters);

            return returnValue;
        }

        public static bool IsNumeric(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var elementType = new NullableConverter(type).UnderlyingType;
                return IsNumeric(elementType);
            }
            return
                type == typeof(Int16) ||
                type == typeof(Int32) ||
                type == typeof(Int64) ||
                type == typeof(UInt16) ||
                type == typeof(UInt32) ||
                type == typeof(UInt64) ||
                type == typeof(decimal) ||
                type == typeof(float) ||
                type == typeof(double);
        }
    }
}
