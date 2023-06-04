using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace AnimeTool.Converters
{
    internal class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is bool)) return null;
            var _value = (bool)value;

            if (targetType == typeof(bool))
            {
                return !_value;
            }

            if (targetType == typeof(Visibility))
            {
                return !_value ? Visibility.Visible : Visibility.Collapsed;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (!(value is bool)) return null;
            return !(bool)value;
        }
    }
}
