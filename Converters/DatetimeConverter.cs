using System;
using Windows.UI.Xaml.Data;

namespace AnimeTool.Converters
{
    internal class DatetimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is string)) return value;
            var _value = value as string;
            var result = _value == "0000-00-00" ? "Unknown" : _value;
            return result;

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (!(value is string)) return value;
            var _value = value as string;
            var result = _value == "Unknown" ? "0000-00-00" : _value;
            return result;
        }
    }
}
