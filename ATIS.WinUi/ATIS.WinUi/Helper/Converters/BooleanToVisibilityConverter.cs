using System;
using System.Globalization;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace ATIS.WinUi.Helper.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public bool Reverse { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var trueValue = !Reverse ? Visibility.Visible : Visibility.Collapsed;
            var falseValue = !Reverse ? Visibility.Collapsed : Visibility.Visible;
            if (value is bool boolValue)
            {
                return boolValue ? trueValue : falseValue;
            }

            return falseValue;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var trueValue = !Reverse ? true : false;
            var falseValue = !Reverse ? false : true;

            if (value is Visibility visibilityValue)
            {
                return visibilityValue == Visibility.Visible ? trueValue : falseValue;
            }

            return falseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
