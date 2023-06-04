using System;
using Windows.UI.Xaml.Data;

namespace AnimeTool.Converters
{
    internal class FloatPercisionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is float) || !(parameter is string)) return value;
            var _value = (float)value;
            var success = int.TryParse(parameter as string, out int _parameter);
            if (!success) return value;
            return MathF.Round(_value, _parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
